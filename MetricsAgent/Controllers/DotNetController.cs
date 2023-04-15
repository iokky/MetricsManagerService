using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MetricsAgent.Controllers;

[Route("api/dotnet")]
[ApiController]
public class DotNetController : ControllerBase
{
    [HttpGet("from/{fromTime}/to/{toTime}")]
    public IActionResult GetDotNetMetric([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime) => Ok();
}
