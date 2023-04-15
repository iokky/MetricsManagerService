using Microsoft.AspNetCore.Mvc;

namespace MetricsAgent.Controllers;

[Route("api/ram")]
[ApiController]
public class RamController : ControllerBase
{
    [HttpGet("available/from/{fromTime}/to/{toTime}")]
    public IActionResult GetRamMetric([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime) => Ok();
}
