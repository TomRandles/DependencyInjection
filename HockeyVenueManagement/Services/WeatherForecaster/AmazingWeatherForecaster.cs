using HockeyVenueManagement.Services;

namespace HockeyVenueManagement.Services
{
    public class AmazingWeatherForecaster : IWeatherForecaster
    {
        public WeatherResult GetCurrentWeather()
        {
            return new WeatherResult
            {
                WeatherCondition = WeatherCondition.Sun
            };
        }
    }
}
