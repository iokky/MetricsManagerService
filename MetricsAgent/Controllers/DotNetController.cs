using MetricsAgent.Logger;
using MetricsAgent.Models.Requests;
using MetricsAgent.Models;
using Microsoft.AspNetCore.Mvc;
using MetricsAgent.Repositories.DotNetRepository;
using MetricsAgent.Models.Dto;
using AutoMapper;

namespace MetricsAgent.Controllers;

[Route("api/dotnet/errors-count")]
[ApiController]
public class DotNetController : ControllerBase
{
    private readonly IDotNetMetricsRepository _repository;
    private readonly IMapper _mapper;
    private readonly IAgentLogger _logger;

    public DotNetController(IDotNetMetricsRepository repository, IMapper mapper, IAgentLogger logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
    }


    [HttpGet("from/{fromTime}/to/{toTime}")]
    public IActionResult GetDotNetMetric([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
    {
        var response = new AllDotNetMetricsResponse() { 
            DotNetMetrics = _repository.GetByRange(fromTime, toTime).Result.Select(i => 
                    _mapper.Map<DotNetMetricsDto>(i)
                ).ToList()
        };

        _logger?.LogDebug($"|{this}| Записи метрик с {fromTime} оп {toTime} получены");
        return Ok(response);
    }

    [HttpGet("all")]
    public IActionResult GetAll()
    {
        var response = new AllDotNetMetricsResponse()
        {
            DotNetMetrics = _repository.GetAll().Select(i =>
                    _mapper.Map<DotNetMetricsDto>(i)
            ).ToList()
        };
        _logger?.LogDebug($"|{this}| Все записи метрик получены");
        return Ok(response);
    }
}
