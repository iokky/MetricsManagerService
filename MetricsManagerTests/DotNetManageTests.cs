using MetricsManagerService.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricsManagerTests;

public class DotNetManageTests
{
    private DotNetController _dotNetController;

    public DotNetManageTests()
    {
        _dotNetController = new DotNetController();
    }

    [Fact]
    public void GetMetricsFromAgent_ReturnOk()
    {
        int agentId = 1;
        TimeSpan fromTime = TimeSpan.FromSeconds(1);
        TimeSpan toTime = TimeSpan.FromSeconds(100);

        IActionResult result = _dotNetController.GetMetricsFromAgent(agentId, fromTime, toTime);

        Assert.IsAssignableFrom<IActionResult>(result);
    }

    [Fact]
    public void GetMetricsFromCluster_ReturnOk()
    {
        TimeSpan fromTime = TimeSpan.FromSeconds(1);
        TimeSpan toTime = TimeSpan.FromSeconds(100);

        IActionResult result = _dotNetController.GetMetricsFromAllCluster(fromTime, toTime);


    }
}