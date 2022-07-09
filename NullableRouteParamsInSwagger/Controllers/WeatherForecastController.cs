using Microsoft.AspNetCore.Mvc;

namespace NullableRouteParamsInSwagger.Controllers;
[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet("{dayOffset?}", Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get(int? dayOffset)
    {
        var validDayOffset = dayOffset.HasValue && dayOffset > 0;
        var start = dayOffset.HasValue && validDayOffset ? dayOffset.Value : 1;
        var count = validDayOffset ? 1 : 5;
        return Enumerable.Range(start, count).Select(index => new WeatherForecast
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }
}
