using MetricsAgent.DAL;
using MetricsAgent.Logger;
using MetricsAgent.Models;

namespace MetricsAgent.Repositories.DotNetRepository
{
    public class DotNetRepository : IDotNetMetricsRepository
    {
        private AgentDbContext _db;
        private readonly IAgentLogger _logger;

        public DotNetRepository(AgentDbContext db, IAgentLogger logger)
        {
            _db = db;
            _logger = logger;

        }
        public async Task Create(DotNetMetrics item)
        {
            await _db.AddAsync(item);
            await _db.SaveChangesAsync();
            _logger?.LogDebug($"|{this}| Запись супешно создана");
            Task.CompletedTask.Wait();
        }

        public IList<DotNetMetrics> GetAll()
        {
            _logger?.LogDebug($"|{this}| Все записи метрик получены");
            return _db.dotNetMetrics.ToList();
        }

        public IList<DotNetMetrics> GetByRange(TimeSpan fromTime, TimeSpan toTime)
        {
            _logger?.LogDebug($"|{this}| Записи метрик с {fromTime} оп {toTime} получены");
            return _db.dotNetMetrics.Where(i => i.Time >= fromTime.TotalSeconds && i.Time <= toTime.TotalSeconds).ToList();
        }
    }
}
