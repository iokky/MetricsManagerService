using MetricsAgent.Repositories.RamRepository;
using Quartz;
using MetricsAgent.Models;
using System.Diagnostics;

namespace MetricsAgent.Jobs;

public class RamMetricsJob : IJob
{
    private readonly IRamRepository _repository;
    private readonly PerformanceCounter _ramCounter;

    public RamMetricsJob(IRamRepository repository)
    {
        _repository = repository;
        _ramCounter = new PerformanceCounter("Memory", "Available MBytes");
    }
    [STAThread]
    public Task Execute(IJobExecutionContext context)
    {
        _ramCounter.NextValue();
        Thread.Sleep(250);
        var value = _ramCounter.NextValue();
        _repository.Create(new RamMetrics()
        {
            Id = Guid.NewGuid(),
            Value = (int)value,
            Time = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds()).TotalSeconds
        });
        
        return Task.CompletedTask;
    }
}
