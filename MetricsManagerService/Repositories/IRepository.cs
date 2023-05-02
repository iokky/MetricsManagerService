using MetricsManagerService.Models;

namespace MetricsManagerService.Repositories;

public interface IRepository<T>
{
    public Task Add(T obj);

    public IEnumerable<T> GetAll();
    public Agent GetById(int id);
    public void SwitchState(int id);

}
