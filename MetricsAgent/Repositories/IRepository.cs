namespace MetricsAgent.Repositories;

public interface IRepository<T> where T : class
{
    public IList<T> GetAll();
    public T GetById(int id);
    public void Create(T item);
    public void Update(T item);
    public void Delete(int id);
    public IList<T> GetByRange(TimeSpan fromTime, TimeSpan toTime);

}
