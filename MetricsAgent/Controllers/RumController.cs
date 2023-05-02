using AutoMapper;
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
    private readonly IMapper _mapper;
    private readonly IAgentLogger _logger;

    public RamController(IRamRepository repository, IMapper mapper, IAgentLogger logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
    }

    [HttpGet("available/from/{fromTime}/to/{toTime}")]
    public IActionResult GetRamMetric([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime) 
    {
        var response = new AllRamMetricsResponse() { 
            RamMetrics = _repository.GetByRange(fromTime, toTime).Select(i =>
                _mapper.Map<RamMetricsDto>(i)
            ).ToList()
        };

        _logger?.LogDebug($"|{this}| Записи метрик с {fromTime} оп {toTime} получены");
        return Ok(response);
    }
    
    [HttpGet("all")]
    public IActionResult GetAll()
    {
        var response = new AllRamMetricsResponse() { 
            RamMetrics = _repository.GetAll().Select(i =>
                _mapper.Map<RamMetricsDto>(i)
            ).ToList()
        };

        _logger?.LogDebug($"|{this}| Все записи метрик получены");
        return Ok(response);
    }
}
