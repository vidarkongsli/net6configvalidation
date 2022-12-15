using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace src.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;
    private readonly IOptions<LocationSettings> _locationSettingsOptions;

    public WeatherForecastController(ILogger<WeatherForecastController> logger,
        IOptions<LocationSettings> locationSettingsOptions)
    {
        _logger = logger;
        _locationSettingsOptions = locationSettingsOptions;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        var locationSettings = _locationSettingsOptions.Value;
        var coordinates = locationSettings.Coordinates.Split(',').Select(Double.Parse);
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)],
            Latitude = coordinates.First(),
            Longitude = coordinates.Last()
        })
        .ToArray();
    }
}
