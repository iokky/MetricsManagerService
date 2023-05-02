using MetricsManagerService.Controllers;
using MetricsManagerService.Models;
using MetricsManagerService.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Xunit.Sdk;

namespace MetricsManagerTests;

/*public class AgentsManagerTests
{
    private AgentsController _agentsController;
    private AgentsRepository _agentsPool;

    public AgentsManagerTests()
    {
        _agentsPool = SingletonAgentPool.GetInstance();
        _agentsController = new AgentsController(_agentsPool);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    public void RegisterAgentTest(int agentId)
    {
        Agent agent  = new() { AgentId = agentId, Enable = true };
        IActionResult result = _agentsController.AgentRegister(agent);
        Assert.IsAssignableFrom<ActionResult>(result);
        Assert.Equal("|agent added|", (result as ObjectResult)?.Value);
    }

    [Fact]
    public void DisableAgentByIdTest()
    {
        int agentId = 3;
        IActionResult result = _agentsController.DisableAgentById(agentId);
        Assert.IsAssignableFrom<ActionResult>(result);
        Assert.Equal(200, (result as ObjectResult)?.StatusCode);
        Assert.Equal("|agent disabled|", (result as ObjectResult)?.Value);
    }

    [Fact]
    public void EnableAgentByIdTest()
    {
        int agentId = 3;
        IActionResult result = _agentsController.EnableAgentById(agentId);
        Assert.IsAssignableFrom<ActionResult>(result);
        Assert.Equal(200, (result as ObjectResult)?.StatusCode);
        Assert.Equal("|agent enabled|", (result as ObjectResult)?.Value);
    }

    [Fact]
    public void GetAgentsTest()
    {
        IActionResult result = _agentsController.GetAll();
        OkObjectResult okObjectResult = Assert.IsAssignableFrom<OkObjectResult>(result);
        Assert.NotNull(okObjectResult.Value as IEnumerable<Agent>);
        Assert.NotEmpty((IEnumerable<Agent>)okObjectResult.Value);
    }
}*/
