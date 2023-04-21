using MetricsAgent.Logger;
using MetricsAgent.Models;
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
        var response = new AllCpuMetricsResponse() { CpuMetrics = _repository.GetByRange(fromTime, toTime).ToList() };
        _logger?.LogDebug($"Записи метрик с {fromTime} оп {toTime} получены");
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

        _logger?.LogDebug($"Успешно добавили новую cpu метрику: {cpuMetric}");
        return Ok();
    }

    [HttpGet("all")]
    public IActionResult GetAll() 
    {
        var response = new AllCpuMetricsResponse() { CpuMetrics = _repository.GetAll().ToList() };

        _logger?.LogDebug("Все записи метрик получены");
        return Ok(response);
    }
   
}

