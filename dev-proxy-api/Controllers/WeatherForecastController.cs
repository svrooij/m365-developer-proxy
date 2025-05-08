using Microsoft.AspNetCore.Mvc;

namespace dev_proxy_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly Worker _worker;
        private readonly IPlugin[] _plugins;
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, Worker worker, IPlugin[] plugins)
        {
            _logger = logger;
            _worker = worker;
            _plugins = plugins;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            foreach (var plugin in _plugins)
            {
                plugin.OnRequest(new RequestEventArgs());
            }
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
