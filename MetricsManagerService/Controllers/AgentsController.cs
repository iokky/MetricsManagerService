using MetricsManagerService.Models;
using MetricsManagerService.Services;
using Microsoft.AspNetCore.Mvc;

namespace MetricsManagerService.Controllers;

[Route("api/agents")]
[ApiController]
public class AgentsController : ControllerBase
{
    private readonly IRepository<IEntity> _pools;
    public AgentsController(IRepository<IEntity> pool)
    {
        _pools = pool;
    }

    [HttpGet("all")]
    public IActionResult GetAll() => Ok(_pools.GetAll());

    [HttpPost("add")]
    public IActionResult AgentRegister([FromBody]Agent agent)
    {
        if (agent != null)
        {
            var msg = _pools.Add(agent);
            if (msg == "|agent added|") { return Ok(msg); }
            else { return BadRequest(_pools.Add(agent)); }
        }
        else
        {
            return BadRequest("empty value");
        }
    }

    [HttpPut("enable/{agentId}")]
    public IActionResult EnableAgentById([FromRoute] int agentId) 
    {
        var msg = _pools.Enable(agentId);
        if (msg == "|agent enabled|") return Ok(msg); 
        else return BadRequest(msg); 
    }

    [HttpPut("disable/{agentId}")]
    public IActionResult DisableAgentById([FromRoute] int agentId)
    {
        var msg = _pools.Disable(agentId);
        if (msg == "|agent disabled|") return Ok(msg); 
        else return BadRequest(msg); 
    }
}
