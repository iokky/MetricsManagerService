using MetricsAgent.DAL;
using MetricsAgent.Models;

namespace MetricsAgent.Repositories.HddRepository
{
    public class HddRepository : IHddRepository
    {
        private readonly AgentDbContext _db;

        public HddRepository(AgentDbContext db)
        {
            _db = db;
        }
        public void Create(HddMetrics item)
        {
            _db.AddAsync(item);
            _db.SaveChangesAsync();
        }

        public void Delete(int id)
        {
            _db.Remove(
                _db.hddMetrics.First(i => i.Id == id)
            );
        }

        public IList<HddMetrics> GetAll()
        {
            return _db.hddMetrics.ToList();
        }

        public HddMetrics GetById(int id)
        {
            return _db.hddMetrics.FirstOrDefault(i => i.Id == id)!;
        }

        public IList<HddMetrics> GetByRange(TimeSpan fromTime, TimeSpan toTime)
        {
            return _db.hddMetrics.Where(i => i.Time >= fromTime.TotalSeconds && i.Time <= toTime.TotalSeconds).ToList();
        }

        public void Update(HddMetrics item)
        {
            _db.Update(item);
        }
    }
}
