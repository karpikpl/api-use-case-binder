
using api.Services.WestCoast;
using common;

namespace api.Services.EastCoast;

[UseCase(UseCaseName.West, 2)]
public class WestWeatherService : IWeatherService
{
    private static readonly string[] Summaries = new[]
    {
        "SEA: Freezing", "SEA: Bracing", "SEA: Chilly", "SEA: Cool", "SEA: Mild", "SEA: Warm", "SEA: Balmy", "SEA: Hot", "SEA: Sweltering", "SEA: Scorching"
    };

    private readonly SeattleService _seattleService;

    public WestWeatherService(SeattleService seattleService)
    {
        _seattleService = seattleService;
    }

    public IEnumerable<WeatherForecast> GetWeatherForecasts()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = _seattleService.GetTemperature(),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }
}