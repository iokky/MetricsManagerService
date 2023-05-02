using MetricsAgent.Repositories.CpuRepository;
using Quartz;
using System.Diagnostics;

namespace MetricsAgent.Jobs;

public class CpuMetricsJob : IJob
{
    private readonly ICpuMetricsRepository _repository;
    private readonly PerformanceCounter _cpuCounter;

    public CpuMetricsJob(ICpuMetricsRepository repository)
    {
        _repository = repository;
        _cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
    }

    [STAThread]
    public Task Execute(IJobExecutionContext context)
    {
        _cpuCounter.NextValue();
        Thread.Sleep(100);
        var value = _cpuCounter.NextValue();
        _repository.Create(new Models.CpuMetrics()
        {
            Id = Guid.NewGuid(),
            Value = (int)value,
            Time = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds()).TotalSeconds
        });

        return Task.CompletedTask;
    }
}
