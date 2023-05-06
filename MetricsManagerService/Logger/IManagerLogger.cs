namespace MetricsManagerService.Logger;

public interface IManagerLogger
{
    void LogError(string message);
    void LogWarning(string message);
    void LogDebug(string message);
    void LogInfo(string message);
}
