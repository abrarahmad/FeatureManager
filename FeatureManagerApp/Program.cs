using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace FeatureManagerApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });

        //Host.CreateDefaultBuilder(args)
        //    .ConfigureWebHostDefaults(webBuilder =>
        //    {
        //        webBuilder.ConfigureAppConfiguration(config =>
        //        {
        //            var settings = config.Build();
        //            var connection = settings.GetConnectionString("AppConfig");
        //            config.AddAzureAppConfiguration(options => options.Connect(connection).UseFeatureFlags(f => f.CacheExpirationInterval = TimeSpan.FromSeconds(1)));
        //        }).UseStartup<Startup>();
        //    });
    }
}
