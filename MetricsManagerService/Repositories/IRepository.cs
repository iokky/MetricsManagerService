using MetricsManagerService.Models;

namespace MetricsManagerService.Repositories;

public interface IRepository<T>
{
    public Task Add(T obj);

    public Task<IEnumerable<T>> GetAll();
    public Agent GetById(int id);
    public void SwitchState(int id);

}
