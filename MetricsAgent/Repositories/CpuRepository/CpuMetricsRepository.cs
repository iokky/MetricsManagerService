using MetricsAgent.DAL;
using MetricsAgent.Logger;
using MetricsAgent.Models;


namespace MetricsAgent.Repositories.CpuRepository;

public class CpuMetricsRepository : ICpuMetricsRepository
{
    private AgentDbContext _db;
    private IAgentLogger _logger;

    public CpuMetricsRepository(AgentDbContext db, IAgentLogger logger)
    {
        _db = db;
        _logger = logger;
    }
    public void Create(CpuMetrics item)
    {
        _db.Add(item);
        _db.SaveChanges();
    }

    public void Delete(int id)
    {
        _db.Remove(
                _db.cpuMetrics.First(i => i.Id == id)
            );
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
    }

    public IList<CpuMetrics> GetByRange(TimeSpan fromTime, TimeSpan toTime)
    {
        var result = _db.cpuMetrics.Where<CpuMetrics>(i => i.Time >= fromTime.TotalSeconds && i.Time <= toTime.TotalSeconds).ToList();

        return result.ToList();
    }
}
