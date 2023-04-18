using MetricsAgent.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace MetricsAgentTests;

public class DotNetAgentTests
{
    private DotNetController _dotNetController;

    public DotNetAgentTests()
    {
        _dotNetController = new DotNetController();
    }

    [Fact]
    public void GetDotNetMetric_ReturnOk()
    {
        TimeSpan fromTime = TimeSpan.FromSeconds(1);
        TimeSpan toTime = TimeSpan.FromSeconds(100);

        IActionResult result = _dotNetController.GetDotNetMetric(fromTime, toTime);
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IActionResult>(result);
    }

}
