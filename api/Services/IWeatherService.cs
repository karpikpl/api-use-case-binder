using common;

namespace api.Services
{
    public interface IWeatherService
    {
        IEnumerable<WeatherForecast> GetWeatherForecasts();
    }
}