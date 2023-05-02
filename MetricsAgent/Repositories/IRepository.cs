using MetricsAgent.Models;

namespace MetricsAgent.Repositories;

public interface IRepository<T> where T : class
{
    public IList<T> GetAll();
    public Task Create(T item);

    public IList<T> GetByRange(TimeSpan fromTime, TimeSpan toTime);

}
