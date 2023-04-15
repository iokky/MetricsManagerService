using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MetricsManagerService.Controllers;

[Route("api/ram")]
[ApiController]
public class RamController : ControllerBase
{

    [HttpGet("agent/{agentId}/available")]
    public IActionResult GetMetricsFromAgent([FromRoute] int agentId) => Ok();

    [HttpGet("cluster/available")]
    public IActionResult GetMetricsFromAllCluster() => Ok();

}
