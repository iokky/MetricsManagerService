using AutoMapper;
using MetricsManagerService.Logger;
using MetricsManagerService.Models.Dto;
using MetricsManagerService.Models.Requests;
using MetricsManagerService.Repositories.CPU;
using Microsoft.AspNetCore.Mvc;

namespace MetricsManagerService.Controllers;

[Route("api/cpu")]
[ApiController]
public class CpuController : ControllerBase
{
    private readonly ICpuMetricsRepository _cpuMetricsRepository;
    private readonly IMapper _mapper;
    private readonly IManagerLogger _logger;


    public CpuController(
        ICpuMetricsRepository cpuMetricsRepository,
        IMapper mapper,
        IManagerLogger logger)
    {
        _cpuMetricsRepository = cpuMetricsRepository;
        _mapper = mapper;
        _logger = logger;
    }

    [HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}")]
    public IActionResult GetMetricsFromAgent([FromRoute] int agentId, [FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime) 
    {
        var response = new CpuMetricsResponse()
        {
            CpuMetrics = _cpuMetricsRepository.GetByRange(agentId, fromTime, toTime).Select(i =>
            _mapper.Map<CpuMetricsDto>(i)).ToArray(),

        };

        return Ok(response);
    }

    [HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}/percentiles/{percentile}")]
    public IActionResult GetMetricsFromAgentByPercentile([FromRoute] int agentId, [FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime, [FromRoute] int persent) => Ok();

    [HttpGet("cluster/from/{fromTime}/to/{toTime}")]
    public IActionResult GetMetricsFromAllCluster([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime) 
    {
        var response = new CpuMetricsResponse() 
        {
            CpuMetrics = _cpuMetricsRepository.GetAllByRange(fromTime, toTime).Select(i =>
            _mapper.Map<CpuMetricsDto>(i)).ToArray(),

        };

        return Ok(response);
    }

}
 