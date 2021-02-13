using HockeyPlazaManagement.Services;

namespace TennisBookings.Web.Services
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
