using MetricsAgent.DAL;
using MetricsAgent.Logger;
using MetricsAgent.Models;

namespace MetricsAgent.Repositories.HddRepository
{
    public class HddRepository : IHddRepository
    {
        private readonly AgentDbContext _db;
        private readonly IAgentLogger _logger;

        public HddRepository(AgentDbContext db, IAgentLogger logger)
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
            return _db.hddMetrics.ToList();
        }

        public IList<HddMetrics> GetByRange(TimeSpan fromTime, TimeSpan toTime)
        {
            _logger?.LogDebug($"|{this}| Записи метрик с {fromTime} оп {toTime} получены");
            return _db.hddMetrics.Where(i => i.Time >= fromTime.TotalSeconds && i.Time <= toTime.TotalSeconds).ToList();
        }
    }
}
