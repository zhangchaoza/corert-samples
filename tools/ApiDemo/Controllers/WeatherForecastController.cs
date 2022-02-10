namespace ApiDemo.Controllers;

using Microsoft.AspNetCore.Mvc;
using ApiDemo.Models;

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
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }

    [HttpPost(Name = "PostWeatherForecast")]
    public IActionResult Post()
    {
        return new JsonResult(new
        {
            status = 0,
            info = "fin"
        });
    }

    [HttpPost("post2")]
    public IActionResult Post2([FromBody] SimpleParams @params)
    {
        _logger.LogInformation(JsonSerializer.Serialize(@params));
        return new JsonResult(new
        {
            status = 0,
            info = "fin"
        });
    }

    [HttpPost("post3")]
    public IActionResult Post3([FromBody] object @params)
    {
        _logger.LogInformation(JsonSerializer.Serialize(@params));
        return new JsonResult(new
        {
            status = 0,
            info = "fin"
        });
    }
}
