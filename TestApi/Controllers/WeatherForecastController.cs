namespace TestApi.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger _logger;

    public WeatherForecastController(ILogger logger)
    {
        _logger = logger;
    }

    [HttpGet("{numberOfDays}", Name = "GetWeatherForecast with numberOfDays")]
    public IActionResult Get([FromRoute] int numberOfDays)
    {
        if (numberOfDays < 1)
        {
            _logger.Error("{NumberOfDays} is less than 1.", numberOfDays);
            return BadRequest();
        }

        _logger.Information("GetWeatherForecast with numberOfDays {NumberOfDays}", numberOfDays);
        if (numberOfDays > 7)
        {
            _logger.Warning("{NumberOfDays} appears more than 1 week, result might not be accurate...", numberOfDays);
        }

        var result = Enumerable.Range(1, numberOfDays).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();

        _logger.Information("Reply with response: {@Result}", result);
        return Ok(new
        {
            Result = result
        });
    }
}
