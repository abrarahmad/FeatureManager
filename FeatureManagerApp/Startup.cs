using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.FeatureManagement;

namespace FeatureManagerApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            //  services.AddAzureAppConfiguration();//use auto refresh azure app-configuraiton the key values
            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;

            });
            //to check percentage featurem comment the session manager registration otherwise it will maintain the session level
            services.AddTransient<ISessionManager, HttpContextFeatureSessionManager>();
            services.AddControllersWithViews(options =>
            {
                // options.Filters.AddForFeature<SlowDownActionFilter>(nameof(FeatureFlags.SlowDown));
            });

            //services.AddFeatureManagement().UseDisabledFeaturesHandler(new CustomDisabledFeatureHandler())
            //    .AddFeatureFilter<PercentageFilter>();
            //services.AddFeatureManagement().UseDisabledFeaturesHandler(new CustomDisabledFeatureHandler())
            //   .AddFeatureFilter<TimeWindowFilter>();
            //services.AddFeatureManagement().UseDisabledFeaturesHandler(new CustomDisabledFeatureHandler())
            //   .AddFeatureFilter<RandomFilter>();

            //services.AddFeatureManagement().UseDisabledFeaturesHandler(new CustomDisabledFeatureHandler())
            //   .AddFeatureFilter<BetaCookieFilter>();
            services.AddFeatureManagement().UseDisabledFeaturesHandler(new CustomDisabledFeatureHandler())
              .AddFeatureFilter<CookiePresentFilter>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            //app.UseAzureAppConfiguration();//use auto refresh azure app-configuraiton the key values
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization(); //use auto refresh the key
            app.UseSession();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
