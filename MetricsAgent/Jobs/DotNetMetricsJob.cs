using MetricsAgent.Repositories.DotNetRepository;
using Quartz;
using System.Diagnostics;

namespace MetricsAgent.Jobs;

public class DotNetMetricsJob : IJob
{
    private readonly IDotNetMetricsRepository _repository;
    private readonly PerformanceCounter _cpuCounter;

    public DotNetMetricsJob(IDotNetMetricsRepository repository)
    {
        _repository = repository;
        _cpuCounter = new PerformanceCounter("DotNet", "% Processor Time", "_Total");
    }
    [STAThread]
    public Task Execute(IJobExecutionContext context)
    {
        _cpuCounter.NextValue();
        Thread.Sleep(100);
        var value = _cpuCounter.NextValue();
        /*        _repository.Create(new Models.CpuMetrics()
                {
                    Value = (int)value,
                    Time = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds()).TotalSeconds
                });*/

        Debug.WriteLine($"|DotNET| {TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds())}|{(int)value}");
        return Task.CompletedTask;
    }
}
