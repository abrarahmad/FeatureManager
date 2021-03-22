using System;
using FeatureFlagConsoleApp;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.FeatureManagement;

IFeatureManager featureManager;

InitializeFeatureJson();
//if (await featureManager.IsEnabledAsync("isProductionEnviornment"))
if (await featureManager.IsEnabledAsync(nameof(FeatureFlags.IsProductionEnviornment)))
{
    Console.WriteLine("Code is running in production environment");
}
//if (await featureManager.IsEnabledAsync("isDevelopmentEnviornment"))
if (await featureManager.IsEnabledAsync(nameof(FeatureFlags.IsDevelopEnviornment)))
{
    Console.WriteLine("Code is running in development environment");
}

Console.ReadKey();


void InitializeFeatureJson()
{
    IConfigurationBuilder builder = new ConfigurationBuilder();
    builder.AddJsonFile("appsettings.json");
    IConfigurationRoot configuration = builder.Build();
    IServiceCollection services = new ServiceCollection();
    services.AddFeatureManagement(configuration);
    IServiceProvider serviceProvider = services.BuildServiceProvider();
    featureManager = serviceProvider.GetRequiredService<IFeatureManager>();
}

//void InitializeFeatureInMemory()
//{
//    var flags = new Dictionary<string, string>
//    {
//        {"FeatureManagement:isProductionEnviornment","false" },
//        {"FeatureManagment:isDevelopEnviornment","true" }
//    };
//    IConfigurationBuilder builder = new ConfigurationBuilder();
//    builder.AddInMemoryCollection(flags);
//    IConfigurationRoot configuration = builder.Build();
//    IServiceCollection services = new ServiceCollection();
//    services.AddFeatureManagement(configuration);
//    IServiceProvider serviceProvider = services.BuildServiceProvider();
//    featureManager = serviceProvider.GetRequiredService<IFeatureManager>();
//}
