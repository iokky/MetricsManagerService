namespace MetricsManagerService.Models;

public class Agent
{
    public int AgentId { get; set; }
    public Uri AgentAddress { get; set; } = null!;
    public bool Enable { get; set; } = false;
}
