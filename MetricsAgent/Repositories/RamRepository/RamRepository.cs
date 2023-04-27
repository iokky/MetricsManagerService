using MetricsAgent.DAL;
using MetricsAgent.Models;

namespace MetricsAgent.Repositories.RamRepository;

public class RamRepository : IRamRepository
{
    private readonly AgentDbContext _db;

    public RamRepository(AgentDbContext db)
    {
        _db = db;
    }
    public void Create(RamMetrics item)
    {
        _db.AddAsync(item);
        _db.SaveChangesAsync();
    }

    public void Delete(int id)
    {
        _db.Remove(
            _db.ramMetrics.First(i => i.Id == id)
        );
    }

    public IList<RamMetrics> GetAll()
    {
        return _db.ramMetrics.ToList();
    }

    public RamMetrics GetById(int id)
    {
        return _db.ramMetrics.FirstOrDefault(i => i.Id == id)!;
    }

    public IList<RamMetrics> GetByRange(TimeSpan fromTime, TimeSpan toTime)
    {
        return _db.ramMetrics.Where(i => i.Time >= fromTime.TotalSeconds && i.Time <= toTime.TotalSeconds).ToList();
    }

    public void Update(RamMetrics item)
    {
        _db.Update(item);
    }
}
