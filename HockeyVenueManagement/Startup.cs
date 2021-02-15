using HockeyVenueManagement.Configuration;
using HockeyVenueManagement.Services;
using HockeyVenueManagement.Services.Audit;
using HockeyVenueManagement.Services.Greetings;
using HockeyVenueManagement.Services.Messaging;
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
            services.Replace(ServiceDescriptor.Singleton<IWeatherForecaster, AmazingWeatherForecaster>());
            // services.RemoveAll<WeatherForecaster>();

            // Add greeting DI
            services.TryAddSingleton<GreetingService>();
            services.TryAddSingleton<IGreetingService>(sp =>
                sp.GetRequiredService<GreetingService>());

            // Add time service DI
            services.TryAddSingleton<IUtcTimeService, TimeService>();

            // Add auditing DI
            services.TryAddScoped(typeof(IAuditor<>), typeof(Auditor<>)); // open generic registration

            // Add Messaging DI
            services.TryAddSingleton<EmailService>();
            services.TryAddSingleton<SMSService>();

            services.AddSingleton<IMessagingService>(sp =>
                new CompositeMessagingService(
                    new IMessagingService[]
                    {
                        sp.GetRequiredService<EmailService>(),
                        sp.GetRequiredService<SMSService>()
                    })); // composite pattern


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
