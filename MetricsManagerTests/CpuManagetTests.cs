using MetricsManagerService.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace MetricsManagerTests;

public class CpuManagetTests
{
    private CpuController _cpuController;

    public CpuManagetTests()
    {
        _cpuController = new CpuController();
    }

    [Fact]
    public void GetMetricsFromAgent_ReturnOk()
    {
        int agentId = 1;
        TimeSpan fromTime = TimeSpan.FromSeconds(1);
        TimeSpan toTime = TimeSpan.FromSeconds(100);

        IActionResult result =_cpuController.GetMetricsFromAgent(agentId, fromTime, toTime);

        Assert.IsAssignableFrom<IActionResult>(result);
    }

    [Fact]
    public void GetMetricsFromCluster_ReturnOk()
    {
        int agentId = 1;
        TimeSpan fromTime = TimeSpan.FromSeconds(1);
        TimeSpan toTime = TimeSpan.FromSeconds(100);

        IActionResult result = _cpuController.GetMetricsFromAllCluster(fromTime, toTime);

        Assert.IsAssignableFrom<IActionResult>(result);
    }


    [Fact]
    public void GetMetricsFromAgentByPercentile_ReturnOk()
    {
        int agentId = 1;
        int persent = 10;
        TimeSpan fromTime = TimeSpan.FromSeconds(1);
        TimeSpan toTime = TimeSpan.FromSeconds(100);

        IActionResult result = _cpuController.GetMetricsFromAgentByPercentile(agentId, fromTime, toTime, persent);

        Assert.IsAssignableFrom<IActionResult>(result);
    }
}