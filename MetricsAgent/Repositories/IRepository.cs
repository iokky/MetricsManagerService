using MetricsAgent.Models;

namespace MetricsAgent.Repositories;

public interface IRepository<T> where T : class
{
    public IEnumerable<T> GetAll();
    public Task Create(T item);
    public Task<IEnumerable<T>> GetByRange(TimeSpan fromTime, TimeSpan toTime);

}
