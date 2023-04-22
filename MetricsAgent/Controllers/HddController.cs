using MetricsAgent.Models.Requests;
using MetricsAgent.Models;
using MetricsAgent.Repositories;
using Microsoft.AspNetCore.Mvc;
using MetricsAgent.Logger;
using MetricsAgent.Repositories.HddRepository;
using MetricsAgent.Models.Dto;

namespace MetricsAgent.Controllers;

[Route("api/hdd")]
[ApiController]
public class HddController : ControllerBase
{
    private readonly IHddRepository _repository;
    private readonly IAgentLogger _logger;
    public HddController(IHddRepository repository, IAgentLogger logger)
    {
        _repository = repository;
        _logger = logger;
    }
    [HttpGet("left/from/{fromTime}/to/{toTime}")]
    public IActionResult GetHddMetric([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime) 
    {
        var response = new AllHddMetricsResponse() { HddMetrics = new List<HddMetricsDto>()};
        foreach (var metric in _repository.GetByRange(fromTime, toTime))
        {
            response.HddMetrics.Add(new HddMetricsDto() 
            {
                Id = metric.Id,
                Value = metric.Value,
                Time = TimeSpan.FromSeconds(metric.Time),
            });
        }
        _logger?.LogDebug($"|HDD| Записи метрик с {fromTime} оп {toTime} получены");
        return Ok(response);
    }

    [HttpPost("create")]
    public IActionResult AddMetrics([FromBody] HddMetricsCreateRequest request)
    {
        var hddNetMetric = new HddMetrics()
        {
            Value = request.Value,
            Time = request.Time.TotalSeconds
        };

        _repository.Create(hddNetMetric);

        _logger?.LogDebug($"|HDD| Успешно добавили новую dotNet метрику: {hddNetMetric}");
        return Ok();
    }

    [HttpGet("all")]
    public IActionResult GetAll()
    {
        var response = new AllHddMetricsResponse() { HddMetrics = new List<HddMetricsDto>()};
        foreach (var metric in _repository.GetAll().ToList())
        {
            response.HddMetrics.Add(new HddMetricsDto()
            {
                Id = metric.Id,
                Value = metric.Value,
                Time = TimeSpan.FromSeconds(metric.Time),
            });
        }
        _logger?.LogDebug("|HDD| Все записи метрик получены");
        return Ok(response);
    }
}
