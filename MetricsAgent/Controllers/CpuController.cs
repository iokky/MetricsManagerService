using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MetricsAgent.Controllers;

[Route("api/cpu")]
[ApiController]
public class CpuController : ControllerBase
{
    [HttpGet("from/{fromTime}/to/{toTime}")]
    public IActionResult GetCpuMetric([FromRoute]TimeSpan fromTime, [FromRoute]TimeSpan toTime) => Ok();
}
