using HockeyVenueManagement.Configuration;
using HockeyVenueManagement.Services;
using HockeyVenueManagement.Services.Greetings;
using HockeyVenueManagement.Services.Time;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;

namespace HockeyVenueManagement
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
            services.AddRazorPages();

            var featuresConfig = Configuration.GetSection("Features");

            services.Configure<FeaturesConfiguration>(featuresConfig);

            // Add weather forcaster service as singleton
            services.TryAddSingleton<IWeatherForecaster, WeatherForecaster>();
            // services.Replace(ServiceDescriptor.Singleton<IWeatherForecaster, AmazingWeatherForecaster>());
            // services.RemoveAll<WeatherForecaster>();

            // Add greeting
            services.TryAddSingleton<GreetingService>();
            services.TryAddSingleton<IGreetingService>(sp =>
                sp.GetRequiredService<GreetingService>());

            // Add time service
            services.TryAddSingleton<IUtcTimeService, TimeService>();

            // Add auditing
            services.TryAddScoped(typeof(IAuditor<>), typeof(Auditor<>)); // open generic registration



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
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
