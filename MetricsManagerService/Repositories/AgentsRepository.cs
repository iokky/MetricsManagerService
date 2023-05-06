using MetricsManagerService.CustomException;
using MetricsManagerService.Models;
using Microsoft.EntityFrameworkCore;

namespace MetricsManagerService.Repositories;

public class AgentsRepository : IAgentRepository
{
    private readonly ServiceAgentDbContext _db;

    public List<Agent> Agents { get; }


    public AgentsRepository(ServiceAgentDbContext db)
    {
        _db = db;
        Agents = _db.Agents.ToList();
    }

    public async Task Add(Agent agent)
    {
        await _db.AddAsync(agent);
        await _db.SaveChangesAsync();
    }

    public async Task<IEnumerable<Agent>> GetAll()
    {
        return await _db.Agents.ToListAsync();
    }


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
