using MetricsAgent.Logger;
using MetricsAgent.Models;
using MetricsAgent.Models.Dto;
using MetricsAgent.Models.Requests;
using MetricsAgent.Repositories.RamRepository;
using Microsoft.AspNetCore.Mvc;

namespace MetricsAgent.Controllers;

[Route("api/ram")]
[ApiController]
public class RamController : ControllerBase
{
    private readonly IRamRepository _repository;
    private readonly IAgentLogger _logger;

    public RamController(IRamRepository repository, IAgentLogger logger)
    {
        _repository = repository;
        _logger = logger;
    }

    [HttpGet("available/from/{fromTime}/to/{toTime}")]
    public IActionResult GetRamMetric([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime) 
    {
        var response = new AllRamMetricsResponse() { RamMetrics = new List<RamMetricsDto>()};
        foreach (var metric in _repository.GetByRange(fromTime, toTime))
        {
            response.RamMetrics.Add(new RamMetricsDto() 
            {
                Id = metric.Id,
                Value = metric.Value,
                Time = TimeSpan.FromSeconds(metric.Time),
            });
        }
        _logger?.LogDebug($"|RAM| Записи метрик с {fromTime} оп {toTime} получены");
        return Ok(response);
    }

    [HttpPost("create")]
    public IActionResult AddMetrics([FromBody] RamMetricsCreateReqest request)
    {
        var ramNetMetric = new RamMetrics()
        {
            Value = request.Value,
            Time = request.Time.TotalSeconds
        };

        _repository.Create(ramNetMetric);

        _logger?.LogDebug($"|RAM| Успешно добавили новую dotNet метрику: {ramNetMetric}");
        return Ok();
    }
    
    [HttpGet("all")]
    public IActionResult GetAll()
    {
        var response = new AllRamMetricsResponse() { RamMetrics = new List<RamMetricsDto>()};
        foreach (var metric in _repository.GetAll())
        {
            response.RamMetrics.Add(new RamMetricsDto()
            {
                Id = metric.Id,
                Value = metric.Value,
                Time = TimeSpan.FromSeconds(metric.Time),
            });
        }
        _logger?.LogDebug("|RAM| Все записи метрик получены");
        return Ok(response);
    }
}
