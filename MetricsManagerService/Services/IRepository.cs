
namespace MetricsManagerService.Services;

public interface IRepository<IEntity>
{
    public Dictionary<int, IEntity> Repository { get; set; }
    public string Add(IEntity obj);

    public IEntity[] GetAll();

    public string Enable(int id);
    public string Disable(int id);

}
