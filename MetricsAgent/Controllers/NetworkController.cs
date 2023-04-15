using Microsoft.AspNetCore.Mvc;

namespace MetricsAgent.Controllers;

[Route("api/network")]
[ApiController]
public class NetworkController : ControllerBase
{
    [HttpGet("from/{fromTime}/to/{toTime}")]
    public IActionResult GetNetworkMetric([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime) => Ok();
}
