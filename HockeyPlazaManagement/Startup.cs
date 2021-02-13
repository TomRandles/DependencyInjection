using HockeyPlazaManagement.Configuration;
using HockeyPlazaManagement.Services;
using HockeyPlazaManagement.Services.Greetings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using System.Linq;
using TennisBookings.Web.Services;

namespace HockeyPlazaManagement
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
            services.AddControllersWithViews();


            var isConfigured = Configuration.GetSection("Feaaatures").Exists();

            var featuresConfig = Configuration.GetSection("Features");

            services.Configure<FeaturesConfiguration>(featuresConfig);
            
            // Add weather forcaster service as singleton
            services.TryAddSingleton<IWeatherForecaster, WeatherForecaster>();
            // services.Replace(ServiceDescriptor.Singleton<IWeatherForecaster, AmazingWeatherForecaster>());
            // services.RemoveAll<WeatherForecaster>();

            // Add greeting
            services.TryAddSingleton<GreetingService>();

            services.TryAddSingleton<IHomePageGreetingService>(sp =>
                sp.GetRequiredService<GreetingService>());

            services.TryAddSingleton<IGreetingService>(sp =>
                sp.GetRequiredService<GreetingService>());



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
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}