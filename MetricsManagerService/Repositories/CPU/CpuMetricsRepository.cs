using MetricsManagerService.Logger;
using MetricsManagerService.Models;
using Microsoft.Extensions.Logging;

namespace MetricsManagerService.Repositories.CPU
{
    public class CpuMetricsRepository : ICpuMetricsRepository
    {
        private readonly ServiceDbContext _db;
        private readonly IManagerLogger _logger;

        public CpuMetricsRepository(ServiceDbContext db, IManagerLogger logger)
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

        public IList<CpuMetrics> GetAll()
        {
            _logger?.LogDebug($"|{this}| Все записи метрик получены");
            return _db.CpuMetrics.ToList();
        }

        public IList<CpuMetrics> GetAllByRange(TimeSpan fromTime, TimeSpan toTime)
        {
            _logger?.LogDebug($"|{this}| Записи метрик с {fromTime} оп {toTime} получены по всем агаентам");
            return _db.CpuMetrics.Where<CpuMetrics>(i =>
                i.Time >= fromTime.TotalSeconds
                    && i.Time <= toTime.TotalSeconds).ToList();
        }

        public IList<CpuMetrics> GetByRange(int agentId, TimeSpan fromTime, TimeSpan toTime)
        {
            _logger?.LogDebug($"|{this}| Записи метрик с {fromTime} оп {toTime} получены для агента {agentId}");
            return _db.CpuMetrics.Where<CpuMetrics>(i => 
                i.AgentId == agentId 
                    && i.Time >= fromTime.TotalSeconds 
                    && i.Time <= toTime.TotalSeconds).ToList();
        }

        public double GetLastTime() 
        {
            try
            {
                return _db.CpuMetrics.Select(i => i.Time).Max();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return 0;
            }
        }
    }
}
