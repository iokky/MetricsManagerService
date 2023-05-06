using AutoMapper;
using MetricsManagerService.Logger;
using MetricsManagerService.Models.Dto;
using MetricsManagerService.Models.Requests;
using MetricsManagerService.Repositories.Ram;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MetricsManagerService.Controllers;

[Route("api/ram")]
[ApiController]
public class RamController : ControllerBase
{
    private readonly IRamMetricsRepository _ramRepository;
    private readonly IMapper _mapper;
    private readonly IManagerLogger _logger;

    public RamController(IRamMetricsRepository repository, IMapper mapper, IManagerLogger logger)
    {
        _ramRepository = repository;
        _mapper = mapper;
        _logger = logger;
    }
    [HttpGet("agent/{agentId}/available/from/{fromTime}/to/{toTime}")]
    public IActionResult GetMetricsFromAgent([FromRoute] int agentId, [FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime) 
    {
        var response = new RamMetricsResponse()
        {
            RamMetrics = _ramRepository.GetByRange(agentId, fromTime, toTime).Select(i =>
            _mapper.Map<RamMetricsDto>(i)).ToArray(),

        };

        return Ok(response);
    }

    [HttpGet("cluster/available")]
    public IActionResult GetMetricsFromAllCluster([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime) 
    {
        var response = new RamMetricsResponse()
        {
            RamMetrics = _ramRepository.GetAllByRange(fromTime, toTime).Select(i =>
            _mapper.Map<RamMetricsDto>(i)).ToArray(),

        };

        return Ok(response);
    }

}
