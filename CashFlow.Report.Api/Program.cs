using CashFlow.Application.Services.Interfaces;
using CashFlow.Application.Services;
using System.Globalization;
using CashFlow.Application.Repositories;
using CashFlow.Infra.Persistence.Repositories;
using Microsoft.AspNetCore.Localization;
using CashFlow.Application.Extensions;
using CashFlow.Infra.Extensions;

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


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.JwtConfig();
builder.Services.SwaggerConfig();

builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
builder.Services.AddScoped<ITransactionService, TransactionService>();

builder.Services.PersistenceConfig(configuration);

var app = builder.Build();

app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture(culture: "pt-BR", uiCulture: "pt-BR"),
    SupportedCultures = supportedCultures,
    SupportedUICultures = supportedCultures
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
