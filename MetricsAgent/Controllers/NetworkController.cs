using AutoMapper;
using MetricsAgent.Logger;
using MetricsAgent.Models;
using MetricsAgent.Models.Dto;
using MetricsAgent.Models.Requests;
using MetricsAgent.Repositories.NetworkRepository;
using Microsoft.AspNetCore.Mvc;

namespace MetricsAgent.Controllers;



[Route("api/network")]
[ApiController]
public class NetworkController : ControllerBase
{
    private readonly INetworkRepository _repository;
    private readonly IMapper _mapper;
    private readonly IAgentLogger _logger;

    public NetworkController(INetworkRepository repository, IMapper mapper, IAgentLogger logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
    }

    [HttpGet("from/{fromTime}/to/{toTime}")]
    public IActionResult GetNetworkMetric([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
    {
        var response = new AllNetworkMetricsResponse() { 
            NetworkMetrics = _repository.GetByRange(fromTime, toTime).Select(i =>
                _mapper.Map<NetworkMetricsDto>(i)
            ).ToList()
        };

        _logger?.LogDebug($"|{this}|Записи метрик с {fromTime} оп {toTime} получены");
        return Ok(response);
    }

    [HttpGet("all")]
    public IActionResult GetAll()
    {
        var response = new AllNetworkMetricsResponse() { 
            NetworkMetrics = _repository.GetAll().Select(i =>
                _mapper.Map<NetworkMetricsDto>(i)
            ).ToList()
        };

        _logger?.LogDebug($"|{this}| Все записи метрик получены");
        return Ok(response);
    }
}
