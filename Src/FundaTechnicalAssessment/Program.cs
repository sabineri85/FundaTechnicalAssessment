using FundaTechnicalAssessment;
using FundaTechnicalAssessment.Core.Interfaces;
using FundaTechnicalAssessment.Core.Services;
using FundaTechnicalAssessment.ExternalIntegrations.HttpClients;
using FundaTechnicalAssessment.ExternalIntegrations.Models.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Http;
using System.Configuration;

var configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)  // or Directory.GetCurrentDirectory()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build();

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        // Register all services here
        services.AddHttpClient<IFundaListingsProvider, FundaHttpClient>();
        services.Configure<PropertySearchSettings>(configuration.GetSection(nameof(PropertySearchSettings)));
        services.AddScoped<IPropertiesService, PropertiesService>();       
    })
    .Build();

// Resolve and run the entry class (not Program.cs itself)
var app = host.Services.GetRequiredService<App>();
await app.RunAsync();