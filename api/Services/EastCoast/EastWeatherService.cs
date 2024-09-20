
using common;

namespace api.Services.EastCoast;

[UseCase(UseCaseName.East, 2)]
public class EastWeatherService : IWeatherService
{
    private static readonly string[] Summaries = new[]
    {
        "NY: Freezing", "NY: Bracing", "NY: Chilly", "NY: Cool", "NY: Mild", "NY: Warm", "NY: Balmy", "NY: Hot", "NY: Sweltering", "NY: Scorching"
    };

    private readonly NewYorkService _newYorkService;

    public EastWeatherService(NewYorkService newYorkService)
    {
        _newYorkService = newYorkService;
    }

    public IEnumerable<WeatherForecast> GetWeatherForecasts()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = _newYorkService.GetTemperature(),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }
}