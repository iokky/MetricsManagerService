using MetricsAgent.Logger;
using MetricsAgent.Models;
using MetricsAgent.Models.Dto;
using MetricsAgent.Models.Requests;
using MetricsAgent.Repositories.CpuRepository;
using Microsoft.AspNetCore.Mvc;


namespace MetricsAgent.Controllers;

[Route("api/cpu")]

[ApiController]
public class CpuController : ControllerBase
{
    private readonly ICpuMetricsRepository _repository;
    private readonly IAgentLogger _logger;

    public CpuController(ICpuMetricsRepository repository, IAgentLogger logger)
    {
        _repository = repository;
        _logger = logger;
    }


    [HttpGet("from/{fromTime}/to/{toTime}")]
    public IActionResult GetCpuMetric([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime) 
    {
        var response = new AllCpuMetricsResponse() { CpuMetrics = new List<CpuMetricsDto>()};
        foreach (var metric in _repository.GetByRange(fromTime, toTime))
        {
            response.CpuMetrics.Add(new CpuMetricsDto()
            {
                Id = metric.Id,
                Value = metric.Value,
                Time = TimeSpan.FromSeconds(metric.Time)
            });  
        }
    

        _logger?.LogDebug($"|CPU| Записи метрик с {fromTime} оп {toTime} получены");
        return Ok(response);
    }

    [HttpPost("create")]
    public IActionResult AddMetrics([FromBody] CpuMetricsCreateRequest request)
    {
        var cpuMetric = new CpuMetrics()
        {
            Value = request.Value,
            Time = request.Time.TotalSeconds
        };

        _repository.Create(cpuMetric);

        _logger?.LogDebug($"|CPU| Успешно добавили новую cpu метрику: {cpuMetric}");
        return Ok();
    }

    [HttpGet("all")]
    public IActionResult GetAll() 
    {
        var response = new AllCpuMetricsResponse() { CpuMetrics = new List<CpuMetricsDto>() };
        foreach (var metric in _repository.GetAll().ToList())
        {
            response.CpuMetrics.Add(new CpuMetricsDto()
            {
                Id = metric.Id,
                Value = metric.Value,
                Time = TimeSpan.FromSeconds(metric.Time)
            });
        }

        _logger?.LogDebug("|CPU| Все записи метрик получены");
        return Ok(response);
    }


    
}

