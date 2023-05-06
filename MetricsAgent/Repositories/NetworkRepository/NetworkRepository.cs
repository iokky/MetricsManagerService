using MetricsAgent.DAL;
using MetricsAgent.Logger;
using MetricsAgent.Models;
using Microsoft.EntityFrameworkCore;

namespace MetricsAgent.Repositories.NetworkRepository;

public class NetworkRepository : INetworkRepository
{
    private readonly AgentDbContext _db;
    private readonly IAgentLogger _logger;
    public NetworkRepository(AgentDbContext db, IAgentLogger logger)
    {
        _db = db;
        _logger = logger;
    }
    public async Task Create(NetworkMetrics item)
    {
        await _db.AddAsync(item);
        await _db.SaveChangesAsync();
        _logger?.LogDebug($"|{this}| Запись супешно создана");
        Task.CompletedTask.Wait();

    }

    public IList<NetworkMetrics> GetAll()
    {
        _logger?.LogDebug($"|{this}| Все записи метрик получены");
        return _db.networkMetrics.ToList();
    }

    IEnumerable<NetworkMetrics> IRepository<NetworkMetrics>.GetAll()
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<NetworkMetrics>> GetByRange(TimeSpan fromTime, TimeSpan toTime)
    {
        _logger?.LogDebug($"|{this}| Записи метрик с {fromTime} оп {toTime} получены");
        return await _db.networkMetrics.Where<NetworkMetrics>(i => i.Time >= fromTime.TotalSeconds && i.Time <= toTime.TotalSeconds).ToListAsync();

    }
}
