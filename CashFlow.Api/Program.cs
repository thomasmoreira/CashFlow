using CashFlow.Application.Services;
using CashFlow.Application.Services.Interfaces;
using CashFlow.Infra.Persistence.Interfaces;
using CashFlow.Infra.Persistence.Repositories;
using FluentValidation;
using CashFlow.Application.Validations;
using CashFlow.Application.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using System.Text.Json;
using CashFlow.Infra.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using JSM.FluentValidation.AspNet.AsyncFilter;
using CashFlow.Application.Common;
using CashFlow.Application.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using CashFlow.Api.Security;

var builder = WebApplication.CreateBuilder(args);

CultureInfo newCulture;
newCulture = new CultureInfo("pt-BR")
{
    NumberFormat = { NumberDecimalSeparator = ",", CurrencyDecimalSeparator = "," },
};

Thread.CurrentThread.CurrentCulture = newCulture;
Thread.CurrentThread.CurrentUICulture = newCulture;
CultureInfo.DefaultThreadCurrentCulture = newCulture;
CultureInfo.DefaultThreadCurrentUICulture = newCulture;

var supportedCultures = new[] { newCulture };

ConfigurationManager configuration = builder.Configuration;
IWebHostEnvironment environment = builder.Environment;

builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

builder.Services.AddControllers().AddModelValidationAsyncActionFilter()
    .ConfigureApiBehaviorOptions(options =>
    {
        options.SuppressModelStateInvalidFilter = true;
    });

var key = Encoding.ASCII.GetBytes(Settings.Secret);
builder.Services.AddAuthentication(o =>
{
    o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(o =>
    {
        o.RequireHttpsMetadata = false;
        o.SaveToken = true;
        o.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false,

        };
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));

builder.Services.AddScoped<ITransactionRespository, TransactionRepository>();

builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<ITransactionService, TransactionService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IJwtUtils, JwtUtils>();

//builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining(typeof(Validator<>));

var host = configuration["DBHOST"] ?? "localhost";
var port = configuration["DBPORT"] ?? "3306";
var password = configuration["MYSQL_PASSWORD"] ?? configuration.GetConnectionString("MYSQL_PASSWORD");
var userid = configuration["MYSQL_USER"] ?? configuration.GetConnectionString("MYSQL_USER");
var cashflowdb = configuration["MYSQL_DATABASE"] ?? configuration.GetConnectionString("MYSQL_DATABASE");

string mySqlConnStr = $"server={host}; userid={userid};pwd={password};port={port};database={cashflowdb}";

builder.Services.AddDbContextPool<CashFlowDbContext>(options =>
     options.UseMySql(mySqlConnStr,
     ServerVersion.AutoDetect(mySqlConnStr)));

builder.Services.AddSingleton<DapperDataContext>();

var app = builder.Build();
app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture(culture: "pt-BR", uiCulture: "pt-BR"),
    SupportedCultures = supportedCultures,
    SupportedUICultures = supportedCultures
});

if (app.Environment.IsDevelopment())
{
    // ensure database is created
    var serviceScope = app.Services.GetService<IServiceScopeFactory>()?.CreateScope();
    var db = serviceScope?.ServiceProvider.GetRequiredService<CashFlowDbContext>();
    db?.Database.EnsureCreated();
    DataInitializer.Run(db);

    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler(appBuilder =>
{
    appBuilder.Run(async context =>
    {
        var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
        if (contextFeature == null) return;

        context.Response.StatusCode = contextFeature.Error switch
        {
            OperationCanceledException => (int)HttpStatusCode.ServiceUnavailable,
            BadRequestException => (int)HttpStatusCode.BadRequest,
            //NotFoundException => (int)HttpStatusCode.NotFound,
            ForbiddenException => (int)HttpStatusCode.Forbidden,
            UnauthorizedException => (int)HttpStatusCode.Unauthorized,
            _ => (int)HttpStatusCode.InternalServerError
        };

        var errorResponse = new
        {
            statusCode = context.Response.StatusCode,
            message = contextFeature.Error.GetBaseException().Message
        };

        await context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse));
    });
});

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();
