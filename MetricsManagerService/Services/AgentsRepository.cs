using MetricsManagerService.CustomException;
using MetricsManagerService.Models;
using MetricsManagerService.Repositories;

namespace MetricsManagerService.Services;

public class AgentsRepository: IAgentRepository
{
    private readonly ServiseDbContext _db;
    public AgentsRepository(ServiseDbContext db)
    {
        _db = db;
    }

    public async Task Add(Agent agent)
    {
        await _db.AddAsync(agent);
        await _db.SaveChangesAsync();
    }

    public IEnumerable<Agent> GetAll() => _db.Agents.ToArray();

    public Agent GetById(int id)
    {
        Agent agent = _db.Agents.FirstOrDefault(a => a.AgentId == id)!;
        if (agent == null)
        {
            throw new NotFoundException();
        }
        return agent;
    }

    public void SwitchState(int id)
    {
        Agent agent = GetById(id);
        if (agent != null)
        {
            if (agent.Enable) agent.Enable = false;
            else agent.Enable = true;
            _db.Update(agent);
        }
    }
}
