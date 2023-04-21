namespace MetricsManagerService.Repositories;

public interface IRepository<T>
{
    public Dictionary<int, T> Repository { get; set; }
    public string Add(T obj);

    public T[] GetAll();

    public string Enable(int id);
    public string Disable(int id);

}
