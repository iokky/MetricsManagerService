using MetricsAgent.Logger;
using MetricsAgent.Models.Requests;
using MetricsAgent.Models;
using Microsoft.AspNetCore.Mvc;
using MetricsAgent.Repositories.DotNetRepository;
using MetricsAgent.Models.Dto;

namespace MetricsAgent.Controllers;

[Route("api/dotnet/errors-count")]
[ApiController]
public class DotNetController : ControllerBase
{
    private readonly IDotNetMetricsRepository _repository;
    private readonly IAgentLogger _logger;

    public DotNetController(IDotNetMetricsRepository repository, IAgentLogger logger)
    {
        _repository = repository;
        _logger = logger;
    }


    [HttpGet("from/{fromTime}/to/{toTime}")]
    public IActionResult GetDotNetMetric([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
    {
        var response = new AllDotNetMetricsResponse() { DotNetMetrics = new List<DotNetMetricsDto>()};
        foreach (var metric in _repository.GetByRange(fromTime, toTime).ToList())
        {
            response.DotNetMetrics.Add(new DotNetMetricsDto() 
            {
                Id = metric.Id,
                Value = metric.Value,
                Time = TimeSpan.FromSeconds(metric.Time),
            });
        }
        _logger?.LogDebug($"|DOTNET| Записи метрик с {fromTime} оп {toTime} получены");
        return Ok(response);
    }

    [HttpPost("create")]
    public IActionResult AddMetrics([FromBody] DotNetMetricsCreateRequest request)
    {
        var dotNetMetric = new DotNetMetrics()
        {
            Value = request.Value,
            Time = request.Time.TotalSeconds
        };

        _repository.Create(dotNetMetric);

        _logger?.LogDebug($"|DOTNET| Успешно добавили новую dotNet метрику: {dotNetMetric}");
        return Ok();
    }

    [HttpGet("all")]
    public IActionResult GetAll()
    {
        var response = new AllDotNetMetricsResponse() { DotNetMetrics = new List<DotNetMetricsDto>()};
        foreach (var metric in _repository.GetAll().ToList())
        {
            response.DotNetMetrics.Add(new DotNetMetricsDto() 
            {
                Id = metric.Id,
                Value = metric.Value,
                Time  = TimeSpan.FromSeconds(metric.Time)
            });
        }
        _logger?.LogDebug("|DOTNET| Все записи метрик получены");
        return Ok(response);
    }
}
