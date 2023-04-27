using MetricsAgent.Repositories.RamRepository;
using Quartz;
using Quartz.Spi;
using System.Diagnostics;

namespace MetricsAgent.Jobs;

public class RamMetricsJob : IJob
{
    private readonly IRamRepository _repository;
    private readonly PerformanceCounter _cpuCounter;

    public RamMetricsJob(IRamRepository repository)
    {
        _repository = repository;
        _cpuCounter = new PerformanceCounter("Memory", "Available MBytes");
    }
    [STAThread]
    public Task Execute(IJobExecutionContext context)
    {
        _cpuCounter.NextValue();
        Thread.Sleep(100);
        var value = _cpuCounter.NextValue();
        _repository.Create(new Models.RamMetrics()
        {
            Value = (int)value,
            Time = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds()).TotalSeconds
        });

        //Debug.WriteLine($"|RAM| {TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds())}| {(int)value} MB");
        
        return Task.CompletedTask;
    }
}
