using MetricsManagerService.Models;
using MetricsManagerService.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace MetricsManagerService.Controllers;

[Route("api/agents")]
[ApiController]
public class AgentsController : ControllerBase
{
    private readonly IAgentRepository _repository;
    public AgentsController(IAgentRepository repository)
    {
        _repository = repository;
    }

    [HttpGet("all")]
    public IActionResult GetAll() => Ok(_repository.GetAll());

    [HttpPost("add")]
    public IActionResult AgentRegister([FromBody]Agent agent)
    {
        _repository.Add(agent);
         return Ok();
    }

    [HttpPut("switch/{agentId}")]
    public IActionResult SwitnAgentById([FromRoute] int agentId)
    {
        _repository.SwitchState(agentId);
        return Ok();
    }
}
