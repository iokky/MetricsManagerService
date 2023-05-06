using AutoMapper;
using MetricsManagerService.Models;
using MetricsManagerService.Models.Dto;
using MetricsManagerService.Models.Requests;
using MetricsManagerService.Repositories;
using MetricsManagerService.Repositories.CPU;
using MetricsManagerService.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace MetricsManagerService.Controllers;

[Route("api/cpu")]
[ApiController]
public class CpuController : ControllerBase
{
    private readonly ICpuMetricsRepository _cpuMetricsRepository;
    private readonly IMapper _mapper;
    

    public CpuController(
        ICpuMetricsRepository cpuMetricsRepository,
        IMapper mapper)
    {
        _cpuMetricsRepository = cpuMetricsRepository;
        _mapper = mapper;
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
 