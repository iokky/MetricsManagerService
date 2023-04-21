using MetricsAgent.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace MetricsAgentTests;

public class HddAgentTests
{
    private HddController _hddController;

    public HddAgentTests()
    {
       // _hddController = new HddController();
    }

    [Fact]
    public void GetHddMetric_ReturnOk()
    {
        TimeSpan fromTime = TimeSpan.FromSeconds(1);
        TimeSpan toTime = TimeSpan.FromSeconds(100);

        IActionResult result = _hddController.GetHddMetric(fromTime, toTime);
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IActionResult>(result);
    }
}
