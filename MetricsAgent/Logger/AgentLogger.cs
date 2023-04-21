using NLog;

namespace MetricsAgent.Logger
{
    public class AgentLogger : IAgentLogger
    {
        private readonly NLog.ILogger _logger = LogManager.GetCurrentClassLogger();
        public void LogDebug(string message)
        {
           _logger.Debug(message);
        }

        public void LogError(string message)
        {
            _logger.Error(message);
        }

        public void LogInfo(string message)
        {
           _logger.Info(message);
        }

        public void LogWarning(string message)
        {
            _logger.Warn(message);
        }
    }
}
