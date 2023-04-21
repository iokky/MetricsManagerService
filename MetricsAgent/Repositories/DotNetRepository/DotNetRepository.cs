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
            _db.Add(item);
            _db.SaveChanges();
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
            return _db.dotNetMetrics.Where(i => Convert.ToInt32(i.Time) >= Convert.ToInt32(fromTime)).ToList();
        }

        public void Update(DotNetMetrics item)
        {
            throw new NotImplementedException();
        }
    }
}
