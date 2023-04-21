using MetricsManagerService.Models;
using MetricsManagerService.Repositories;

namespace MetricsManagerService.Services;

public class AgentsPool: IRepository<Agent>
{
    private Dictionary<int, Agent> _repos;
    public Dictionary<int, Agent> Repository { get { return _repos; } set { _repos = value; } }
    public AgentsPool()
    {
        _repos = new Dictionary<int, Agent>();
    }

    public string Add(Agent agent)
    {
        if (!_repos.ContainsKey(agent.AgentId))
        {
            _repos.Add(agent.AgentId, agent);
            return "|agent added|";
        }
        return $"|agent already exist| agentID: {agent.AgentId} agentUri: {_repos[agent.AgentId].AgentAddress} ";
    }

    public string Enable(int agentId)
    {
        if (_repos.ContainsKey(agentId))
        {
            if (_repos[agentId].Enable == true)
            {          
                return "|already enabled|";
            }
            else
            {
                _repos[agentId].Enable = true;
                return "|agent enabled|";
            }
        }
        return "|agent not registred| for registred agent use ../add ";
    }

    public string Disable(int agentId)
    {
        if (_repos.ContainsKey(agentId))
        {
            if (_repos[agentId].Enable != true)
            {
                return "|already disabled|";
            }
            else
            {
                _repos[agentId].Enable = false;
                return "|agent disabled|";
            }
        }
        return "|agent not registred| for registred agent use ../add ";
    }

    public Agent[] GetAll() => _repos.Values.ToArray();
}
