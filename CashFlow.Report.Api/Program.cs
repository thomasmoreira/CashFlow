using CashFlow.Application.Services.Interfaces;
using CashFlow.Application.Services;
using System.Globalization;
using CashFlow.Application.Repositories;
using CashFlow.Infra.Persistence.Repositories;
using CashFlow.Infra.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Localization;

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
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ITransactionRespository, TransactionRepository>();
builder.Services.AddScoped<ITransactionService, TransactionService>();

var host = configuration["DBHOST"] ?? "localhost";
var port = configuration["DBPORT"] ?? "3306";
var password = configuration["MYSQL_PASSWORD"] ?? configuration.GetConnectionString("MYSQL_PASSWORD");
var userid = configuration["MYSQL_USER"] ?? configuration.GetConnectionString("MYSQL_USER");
var cashflowdb = configuration["MYSQL_DATABASE"] ?? configuration.GetConnectionString("MYSQL_DATABASE");

string mySqlConnStr = $"server={host}; userid={userid};pwd={password};port={port};database={cashflowdb}";


builder.Services.AddDbContextPool<CashFlowDbContext>(options =>
     options.UseMySql(mySqlConnStr,
     ServerVersion.AutoDetect(mySqlConnStr)));

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
