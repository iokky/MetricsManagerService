using MetricsAgent.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace MetricsAgentTests;

public class CpuAgentTests
{
    private CpuController _cpuController;

    public CpuAgentTests()
    {
        _cpuController = new CpuController();
    }
    [Fact]
    public void GetCpuMetric_ReturnOk()
    {
        TimeSpan fromTime = TimeSpan.FromSeconds(1);
        TimeSpan toTime = TimeSpan.FromSeconds(10);

        IActionResult result = _cpuController.GetCpuMetric(fromTime, toTime);
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IActionResult>(result);
    }

}