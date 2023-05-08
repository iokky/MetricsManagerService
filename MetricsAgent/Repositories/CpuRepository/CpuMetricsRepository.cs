using MetricsAgent.DAL;
using MetricsAgent.Logger;
using MetricsAgent.Models;
using Microsoft.EntityFrameworkCore;

namespace MetricsAgent.Repositories.CpuRepository;

public class CpuMetricsRepository : ICpuMetricsRepository
{
    private readonly AgentDbContext _db;
    private readonly IAgentLogger _logger;

    public CpuMetricsRepository(AgentDbContext db, IAgentLogger logger)
    {
        _db = db;
        _logger = logger;
    }
    public async Task Create(CpuMetrics item)
    {
        await _db.AddAsync(item);
        await _db.SaveChangesAsync();
        _logger?.LogDebug($"|{this}| Запись супешно создана");
        Task.CompletedTask.Wait();
    }


    public IEnumerable<CpuMetrics> GetAll()
    {
        _logger?.LogDebug($"|{this}| Все записи метрик получены");
        return _db.cpuMetrics.ToList();
    }

    public async Task<IEnumerable<CpuMetrics>> GetByRange(TimeSpan fromTime, TimeSpan toTime)
    {
        _logger?.LogDebug($"|{this}| Записи метрик с {fromTime} оп {toTime} получены");
        return await _db.cpuMetrics.Where<CpuMetrics>(i => i.Time >= fromTime.TotalSeconds && i.Time <= toTime.TotalSeconds).ToListAsync();
    }
}
