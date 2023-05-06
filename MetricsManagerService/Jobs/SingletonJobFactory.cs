using Quartz;
using Quartz.Spi;

namespace MetricsManagerService.Jobs;

public class SingletonJobFactory : IJobFactory
{
    protected readonly IServiceScopeFactory _serviceScopeFactory;

    public SingletonJobFactory(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    public IJob? NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
    {
        using(var scope = _serviceScopeFactory.CreateScope())
        {
           return scope.ServiceProvider.GetService(bundle.JobDetail.JobType) as IJob;
        }
    }

    public void ReturnJob(IJob job) { }
}
