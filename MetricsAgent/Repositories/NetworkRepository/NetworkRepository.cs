using MetricsAgent.DAL;
using MetricsAgent.Models;

namespace MetricsAgent.Repositories.NetworkRepository;

public class NetworkRepository : INetworkRepository
{
    private readonly AgentDbContext _db;
    public NetworkRepository(AgentDbContext db)
    {
        _db = db;
    }
    public void Create(NetworkMetrics item)
    {
        _db.AddAsync(item);
        _db.SaveChangesAsync();
    }

    public void Delete(int id)
    {
        _db.Remove(
             _db.networkMetrics.First(i => i.Id == id)
         );
    }

    public IList<NetworkMetrics> GetAll()
    {
        return _db.networkMetrics.ToList();
    }

    public NetworkMetrics GetById(int id)
    {
        return _db.networkMetrics.FirstOrDefault(i => i.Id == id)!;
    }

    public IList<NetworkMetrics> GetByRange(TimeSpan fromTime, TimeSpan toTime)
    {
        return _db.networkMetrics.Where(i => i.Time >= fromTime.TotalSeconds && i.Time <= toTime.TotalSeconds).ToList();
    }

    public void Update(NetworkMetrics item)
    {
        _db.Update(item);   
    }
}
