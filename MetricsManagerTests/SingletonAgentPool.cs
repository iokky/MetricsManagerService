using MetricsManagerService.Services;

namespace MetricsManagerTests;

public class SingletonAgentPool
{
    private static AgentsPool _instance;

    private SingletonAgentPool()
    {
        
    }

    public static AgentsPool GetInstance()
    {
        if (_instance == null)
        {
            _instance = new AgentsPool();
        }
        return _instance;   
    }
}
