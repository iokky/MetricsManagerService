using Microsoft.AspNetCore.Mvc;

namespace MetricsAgent.Controllers;

[Route("api/hdd")]
[ApiController]
public class HddController : ControllerBase
{
    [HttpGet("left/from/{fromTime}/to/{toTime}")]
    public IActionResult GetHddMetric([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime) => Ok();
}
