using FluentValidation;
using CashFlow.Application.Validations;
using CashFlow.Application.Exceptions;
using CashFlow.Infra.Persistence;
using Microsoft.AspNetCore.Localization;
using System.Globalization;
using CashFlow.Application.Extensions;
using CashFlow.Infra.Extensions;
using Serilog;

try
{

    var builder = WebApplication.CreateBuilder(args);

    ConfigurationManager configuration = builder.Configuration;
    IWebHostEnvironment environment = builder.Environment;

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

    builder.Host.UseSerilog((ctx, loggerConfiguration) =>
    {
        loggerConfiguration
            .ReadFrom.Configuration(ctx.Configuration)
            .Enrich.FromLogContext()
            .Enrich.WithProperty("ApplicationName", typeof(Program).Assembly.GetName().Name)
            .Enrich.WithProperty("Environment", ctx.HostingEnvironment);
    });

    builder.Services.AddLogging();

    builder.Services.ApplicationConfig();

    builder.Services.JwtConfig();

    builder.Services.AddSwaggerGen();

    builder.Services.PersistenceConfig(configuration);

    builder.Services.AddValidatorsFromAssemblyContaining(typeof(Validator<>));

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

    app.UseSerilogRequestLogging();

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();

    app.UseMiddleware<ErrorHandlingMiddleware>();

    app.Run();
}
catch (Exception ex)
{

    if (Log.Logger == null || Log.Logger.GetType().Name == "SilentLogger")
    {        
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Console()
            .CreateLogger();
    }

    Log.Fatal(ex, "Host terminated unexpectedly");
}
finally
{
    Log.Information("Shut down complete");
    Log.CloseAndFlush();
}

