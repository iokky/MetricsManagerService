using MetricsAgent.DAL;
using MetricsAgent.Logger;
using MetricsAgent.Models;
using Microsoft.EntityFrameworkCore;

namespace MetricsAgent.Repositories.RamRepository;

public class RamRepository : IRamRepository
{
    private readonly AgentDbContext _db;
    private readonly IAgentLogger _logger;


    public RamRepository(AgentDbContext db, IAgentLogger logger)
    {
        _db = db;
        _logger = logger;
    }

    public async Task Create(RamMetrics item)
    {
        await _db.AddAsync(item);
        await _db.SaveChangesAsync();
        _logger?.LogDebug($"|{this}| Запись супешно создана");
        Task.CompletedTask.Wait();
    }

    public IList<RamMetrics> GetAll()
    {
        _logger?.LogDebug($"|{this}| Все записи метрик получены");
        return _db.ramMetrics.ToList();
    }

    IEnumerable<RamMetrics> IRepository<RamMetrics>.GetAll()
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<RamMetrics>> GetByRange(TimeSpan fromTime, TimeSpan toTime)
    {
        return await _db.ramMetrics.Where<RamMetrics>(i => i.Time >= fromTime.TotalSeconds && i.Time <= toTime.TotalSeconds).ToListAsync();

    }
}
