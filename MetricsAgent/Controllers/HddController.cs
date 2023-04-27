using MetricsAgent.Models.Requests;
using MetricsAgent.Models;
using MetricsAgent.Repositories;
using Microsoft.AspNetCore.Mvc;
using MetricsAgent.Logger;
using MetricsAgent.Repositories.HddRepository;
using MetricsAgent.Models.Dto;
using AutoMapper;

namespace MetricsAgent.Controllers;

[Route("api/hdd")]
[ApiController]
public class HddController : ControllerBase
{
    private readonly IHddRepository _repository;
    private readonly IMapper _mapper;
    private readonly IAgentLogger _logger;
    public HddController(IHddRepository repository, IMapper mapper, IAgentLogger logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
    }

    [HttpGet("left/from/{fromTime}/to/{toTime}")]
    public IActionResult GetHddMetric([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime) 
    {
        var response = new AllHddMetricsResponse() { 
            HddMetrics = _repository.GetByRange(fromTime, toTime).Select(i =>
                _mapper.Map<HddMetricsDto>(i)
            ).ToList()
        };

        _logger?.LogDebug($"|HDD| Записи метрик с {fromTime} оп {toTime} получены");
        return Ok(response);
    }

    [HttpGet("all")]
    public IActionResult GetAll()
    {
        var response = new AllHddMetricsResponse() { 
            HddMetrics = _repository.GetAll().ToList().Select(i =>
                _mapper.Map<HddMetricsDto>(i)
            ).ToList()
        };

        _logger?.LogDebug("|HDD| Все записи метрик получены");
        return Ok(response);
    }
}
