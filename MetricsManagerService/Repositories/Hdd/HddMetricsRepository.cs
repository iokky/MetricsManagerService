using MetricsManagerService.Logger;
using MetricsManagerService.Models;

namespace MetricsManagerService.Repositories.Hdd
{
    public class HddMetricsRepository : IHddMetricsRepository
    {
        private readonly ServiceDbContext _db;
        private readonly IManagerLogger _logger;

        public HddMetricsRepository(ServiceDbContext db, IManagerLogger logger)
        {
            _db = db;
            _logger = logger;
        }
        public async Task Create(HddMetrics item)
        {
            await _db.AddAsync(item);
            await _db.SaveChangesAsync();
            _logger?.LogDebug($"|{this}| Запись супешно создана");
            Task.CompletedTask.Wait();
        }

        public IList<HddMetrics> GetAll()
        {
            _logger?.LogDebug($"|{this}| Все записи метрик получены");
            return _db.HddMetrics.ToList();
        }

        public IList<HddMetrics> GetAllByRange(TimeSpan fromTime, TimeSpan toTime)
        {
            _logger?.LogDebug($"|{this}| Записи метрик с {fromTime} оп {toTime} получены по всем агаентам");
            return _db.HddMetrics.Where<HddMetrics>(i =>
                i.Time >= fromTime.TotalSeconds
                    && i.Time <= toTime.TotalSeconds).ToList();
        }

        public IList<HddMetrics> GetByRange(int agentId, TimeSpan fromTime, TimeSpan toTime)
        {
            _logger?.LogDebug($"|{this}| Записи метрик с {fromTime} оп {toTime} получены для агента {agentId}");
            return _db.HddMetrics.Where<HddMetrics>(i =>
                i.AgentId == agentId
                    && i.Time >= fromTime.TotalSeconds
                    && i.Time <= toTime.TotalSeconds).ToList();
        }

        public double GetLastTime()
        {
            try
            {
                return _db.HddMetrics.Select(i => i.Time).Max();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return 0;
            }
        }
    }
}
