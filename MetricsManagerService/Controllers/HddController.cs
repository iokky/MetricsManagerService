using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MetricsManagerService.Controllers
{
    [Route("api/hdd")]
    [ApiController]
    public class HddController : ControllerBase
    {
        [HttpGet("agent/{agentId}/left")]
        public IActionResult GetMetricsFromAgent([FromRoute] int agentId) => Ok();

        [HttpGet("cluster/left")]
        public IActionResult GetMetricsFromAllCluster() => Ok();
    }
}
