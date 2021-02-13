using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using HockeyVenueManagement.Models;
using HockeyVenueManagement.Services;
using HockeyVenueManagement.Configuration;
using Microsoft.Extensions.Options;

namespace HockeyVenueManagement.Controllers
{
    public class HomeController : Controller
    {

        private readonly IWeatherForecaster _weatherForecaster;
        private readonly FeaturesConfiguration _featuresConfiguration;
        private readonly ILogger<HomeController> _logger;

        public HomeController(IWeatherForecaster weatherForecaster,
            IOptions<FeaturesConfiguration> options,
            ILogger<HomeController> logger)
        {
            _weatherForecaster = weatherForecaster;
            _featuresConfiguration = options.Value;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var viewModel = new HomeViewModel();

            if (_featuresConfiguration.EnabledWeatherForecast)
            {
                var currentWeather = _weatherForecaster.GetCurrentWeather();

                switch (currentWeather.WeatherCondition)
                {
                    case WeatherCondition.Sun:
                        viewModel.WeatherDescription = "It's sunny right now. A great day for tennis.";
                        break;

                    case WeatherCondition.Rain:
                        viewModel.WeatherDescription = "We're sorry but it's raining here. No outdoor courts in use.";
                        break;

                    default:
                        viewModel.WeatherDescription = "We don't have the latest weather information right now, please check again later.";
                        break;
                }
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
