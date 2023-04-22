/*using MetricsAgent.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace MetricsAgentTests;

public class RamAgentTests
{
    private RamController _ramController;

    public RamAgentTests()
    {
        _ramController = new RamController();
    }

    [Fact]
    public void GetRamMetric()
    {
        TimeSpan fromTime = TimeSpan.FromSeconds(1);
        TimeSpan toTime = TimeSpan.FromSeconds(100);

        IActionResult result = _ramController.GetRamMetric(fromTime, toTime);
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IActionResult>(result);
    }
}*/
