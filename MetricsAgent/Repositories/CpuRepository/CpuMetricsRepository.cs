using MetricsAgent.DAL;
using MetricsAgent.Models;


namespace MetricsAgent.Repositories.CpuRepository;

public class CpuMetricsRepository : ICpuMetricsRepository
{
    private readonly AgentDbContext _db;

    public CpuMetricsRepository(AgentDbContext db)
    {
        _db = db;
    }
    public void Create(CpuMetrics item)
    {
        _db.AddAsync(item);
        _db.SaveChangesAsync();
    }

    public void Delete(int id)
    {       
        _db.cpuMetrics.Remove(GetById(id));
        _db.SaveChanges();
    }

    public IList<CpuMetrics> GetAll()
    {
        return _db.cpuMetrics.ToList();
    }

    public CpuMetrics GetById(int id)
    {
        return _db.cpuMetrics.FirstOrDefault(i => i.Id == id)!;
    }

    public void Update(CpuMetrics item)
    {
        _db.Update(item);
        _db.SaveChanges();
    }

    public IList<CpuMetrics> GetByRange(TimeSpan fromTime, TimeSpan toTime)
    {
        return _db.cpuMetrics.Where<CpuMetrics>(i => i.Time >= fromTime.TotalSeconds && i.Time <= toTime.TotalSeconds).ToList();       
    }
}
