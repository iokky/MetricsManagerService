using MetricsAgent.DAL;
using MetricsAgent.Logger;
using MetricsAgent.Models;
using Microsoft.EntityFrameworkCore;

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
        public IEnumerable<HddMetrics> GetAll()
        {
            _logger?.LogDebug($"|{this}| Все записи метрик получены");
            return _db.hddMetrics.ToList();
        }

        public async Task<IEnumerable<HddMetrics>> GetByRange(TimeSpan fromTime, TimeSpan toTime)
        {
            _logger?.LogDebug($"|{this}| Записи метрик с {fromTime} оп {toTime} получены");
            return await _db.hddMetrics.Where<HddMetrics>(i => i.Time >= fromTime.TotalSeconds && i.Time <= toTime.TotalSeconds).ToListAsync();
        }


    }
}
