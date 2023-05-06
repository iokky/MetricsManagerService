using MetricsManagerService.Logger;
using MetricsManagerService.Models;

namespace MetricsManagerService.Repositories.Ram
{
    public class RamMetricsRepository : IRamMetricsRepository
    {
        private readonly ServiceDbContext _db;
        private readonly IManagerLogger _logger;

        public RamMetricsRepository(ServiceDbContext db, IManagerLogger logger)
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
            return _db.RamMetrics.ToList();
        }

        public IList<RamMetrics> GetAllByRange(TimeSpan fromTime, TimeSpan toTime)
        {
            _logger?.LogDebug($"|{this}| Записи метрик с {fromTime} оп {toTime} получены по всем агаентам");
            return _db.RamMetrics.Where<RamMetrics>(i =>
                i.Time >= fromTime.TotalSeconds
                    && i.Time <= toTime.TotalSeconds).ToList();
        }

        public IList<RamMetrics> GetByRange(int agentId, TimeSpan fromTime, TimeSpan toTime)
        {
            _logger?.LogDebug($"|{this}| Записи метрик с {fromTime} оп {toTime} получены для агента {agentId}");
            return _db.RamMetrics.Where<RamMetrics>(i =>
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
