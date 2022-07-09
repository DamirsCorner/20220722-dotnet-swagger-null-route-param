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

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        return this.Get(1, 5);
    }

    [HttpGet("{dayOffset}", Name = "GetWeatherForecastForOneDay")]
    public IEnumerable<WeatherForecast> Get(int dayOffset)
    {
        return dayOffset > 0 ? this.Get(dayOffset, 1) : this.Get();
    }

    private IEnumerable<WeatherForecast> Get(int start, int count)
    {
        return Enumerable.Range(start, count).Select(index => new WeatherForecast
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }
}
