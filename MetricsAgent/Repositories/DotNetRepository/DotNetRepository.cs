using MetricsAgent.DAL;
using MetricsAgent.Models;

namespace MetricsAgent.Repositories.DotNetRepository
{
    public class DotNetRepository : IDotNetMetricsRepository
    {
        private AgentDbContext _db;

        public DotNetRepository(AgentDbContext db)
        {
            _db = db;

        }
        public void Create(DotNetMetrics item)
        {
            _db.AddAsync(item);
            _db.SaveChangesAsync();
        }

        public void Delete(int id)
        {
            _db.Remove(
                _db.dotNetMetrics.First(i => i.Id == id)
            );
        }

        public IList<DotNetMetrics> GetAll()
        {
            return _db.dotNetMetrics.ToList();
        }

        public DotNetMetrics GetById(int id)
        {
            return _db.dotNetMetrics.FirstOrDefault(i => i.Id == id)!;
        }

        public IList<DotNetMetrics> GetByRange(TimeSpan fromTime, TimeSpan toTime)
        {
            return _db.dotNetMetrics.Where(i => i.Time >= fromTime.TotalSeconds && i.Time <= toTime.TotalSeconds).ToList();
        }

        public void Update(DotNetMetrics item)
        {
            _db.Update(item);
        }
    }
}
