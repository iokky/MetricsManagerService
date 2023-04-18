using MetricsAgent.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace MetricsAgentTests;

public class NetworkAgentTests
{
    private NetworkController _networkController;
    public NetworkAgentTests()
    {
        _networkController = new NetworkController();
    }

    [Fact]

    public void GetNetworkMetric_ReturnOk()
    {
        TimeSpan fromTime = TimeSpan.FromSeconds(1);
        TimeSpan toTime = TimeSpan.FromSeconds(100);

        IActionResult result = _networkController.GetNetworkMetric(fromTime, toTime);
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IActionResult>(result);
    }
}
