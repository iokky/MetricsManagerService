using MetricsAgent.Controllers;
using MetricsAgent.Logger;
using MetricsAgent.Repositories.CpuRepository;
using MetricsAgent.Repositories;
using Microsoft.AspNetCore.Mvc;
using MetricsAgent.Models;
using MetricsAgent.Models.Requests;

namespace MetricsAgentTests;

public class CpuAgentTests
{
    private CpuController _cpuController;

    public CpuAgentTests(ICpuMetricsRepository repository, IAgentLogger logger)
    {
        _cpuController = new CpuController(repository, logger);
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