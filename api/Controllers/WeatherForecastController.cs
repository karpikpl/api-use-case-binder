using api.Services;
using common;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet("GetWeatherForecast/{useCase}")]
    [UseCaseAuthorize(UseCaseName.West, PolicyName = AuthPolicyName.WestCoastPolicy)]
    [UseCaseAuthorize(UseCaseName.East, PolicyName = AuthPolicyName.EastCoastPolicy)]
    public IEnumerable<WeatherForecast> Get([FromService] IWeatherService weatherService)
    {
        return weatherService.GetWeatherForecasts();
    }
}
