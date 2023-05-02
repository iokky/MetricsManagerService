using MetricsManagerService.Models;
using MetricsManagerService.Models.Requests;
using MetricsManagerService.Repositories;
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
    private readonly IAgentRepository _repository;
    private readonly IMerticsAgentClient _agentClient;
    

    public CpuController(IAgentRepository repository, IMerticsAgentClient agentClient)
    {
        _repository = repository;
        _agentClient = agentClient;
    }

    [HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}")]
    public IActionResult GetMetricsFromAgent([FromRoute] int agentId, [FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime) 
    {
     
        return Ok(_agentClient.GetCpuMetrics(new CpuMetricsRequest()
        {
            AgentId = agentId,
            FromTime = fromTime,
            ToTime = toTime
        }));
    }

    [HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}/percentiles/{percentile}")]
    public IActionResult GetMetricsFromAgentByPercentile([FromRoute] int agentId, [FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime, [FromRoute] int persent) => Ok();

    [HttpGet("cluster/from/{fromTime}/to/{toTime}")]
    public IActionResult GetMetricsFromAllCluster([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime) 
    {

        return Ok();
    }

}
 