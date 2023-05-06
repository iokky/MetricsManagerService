using MetricsManagerService.Logger;
using MetricsManagerService.Models;

namespace MetricsManagerService.Repositories.Network
{
    public class NetworkMetricsRepository : INetworkMetricsRepository
    {
        private readonly ServiceDbContext _db;
        private readonly IManagerLogger _logger;
        public NetworkMetricsRepository(ServiceDbContext db, IManagerLogger logger)
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
            return _db.NetworkMetrics.ToList();
        }

        public IList<NetworkMetrics> GetAllByRange(TimeSpan fromTime, TimeSpan toTime)
        {
            _logger?.LogDebug($"|{this}| Записи метрик с {fromTime} оп {toTime} получены по всем агаентам");
            return _db.NetworkMetrics.Where<NetworkMetrics>(i =>
                i.Time >= fromTime.TotalSeconds
                    && i.Time <= toTime.TotalSeconds).ToList();
        }

        public IList<NetworkMetrics> GetByRange(int agentId, TimeSpan fromTime, TimeSpan toTime)
        {
            _logger?.LogDebug($"|{this}| Записи метрик с {fromTime} оп {toTime} получены для агента {agentId}");
            return _db.NetworkMetrics.Where<NetworkMetrics>(i =>
                i.AgentId == agentId
                    && i.Time >= fromTime.TotalSeconds
                    && i.Time <= toTime.TotalSeconds).ToList();
        }

        public double GetLastTime()
        {
            try
            {
                return _db.NetworkMetrics.Select(i => i.Time).Max();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return 0;
            }
        }
    }
}
