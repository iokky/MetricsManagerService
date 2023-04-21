using MetricsAgent.Logger;
using MetricsAgent.Models.Requests;
using MetricsAgent.Models;
using MetricsAgent.Repositories.CpuRepository;
using Microsoft.AspNetCore.Mvc;
using MetricsAgent.Repositories.DotNetRepository;

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
        var response = new AllDotNetMetricsResponse() { DotNetMetrics = _repository.GetByRange(fromTime, toTime).ToList() };
        return Ok(response);
    }

    [HttpPost("create")]
    public IActionResult AddMetrics([FromBody] DotNetMetricsCreateRequest request)
    {
        var dotNetMetric = new DotNetMetrics()
        {
            Value = request.Value,
            Time = request.Time
        };

        _repository.Create(dotNetMetric);

        _logger?.LogDebug($"Успешно добавили новую dotNet метрику: {dotNetMetric}");
        return Ok();
    }

    [HttpGet("all")]
    public IActionResult GetAll()
    {
        //var response = new AllCpuMetricsResponse() { CpuMetrics = _repository.GetAll().ToList() };
        var response = "ok";
        _logger?.LogDebug("Все записи метрик получены");
        return Ok(response);
    }
}
