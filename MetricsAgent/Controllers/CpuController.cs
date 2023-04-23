using AutoMapper;
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
    private readonly IMapper _mapper;
    private readonly IAgentLogger _logger;

    public CpuController(ICpuMetricsRepository repository, IMapper mapper, IAgentLogger logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
    }


    [HttpGet("from/{fromTime}/to/{toTime}")]
    public IActionResult GetCpuMetric([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime) 
    {
        var response = new AllCpuMetricsResponse() { 
            CpuMetrics = _repository.GetByRange(fromTime, toTime).Select(i =>
                _mapper.Map<CpuMetricsDto>(i)
            ).ToList()
        };
        
        _logger?.LogDebug($"|CPU| Записи метрик с {fromTime} оп {toTime} получены");
        return Ok(response);
    }

    [HttpPost("create")]
    public IActionResult AddMetrics([FromBody] CpuMetricsCreateRequest request)
    {
        var cpuMetric = _mapper.Map<CpuMetrics>(request);
        _repository.Create(cpuMetric);

        _logger?.LogDebug($"|CPU| Успешно добавили новую cpu метрику: {cpuMetric}");
        return Ok();
    }

    [HttpGet("all")]
    public IActionResult GetAll()
    {
        var response = new AllCpuMetricsResponse()
        {
            CpuMetrics = _repository.GetAll().Select(i =>
                _mapper.Map<CpuMetricsDto>(i)).ToList()
        }; 

        _logger?.LogDebug("|CPU| Все записи метрик получены");
        return Ok(response);
    }
}

