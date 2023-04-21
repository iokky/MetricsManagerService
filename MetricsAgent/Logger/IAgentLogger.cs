namespace MetricsAgent.Logger;

public interface IAgentLogger
{
    void LogError(string message);
    void LogWarning(string message);
    void LogDebug(string message);
    void LogInfo(string message);
}
