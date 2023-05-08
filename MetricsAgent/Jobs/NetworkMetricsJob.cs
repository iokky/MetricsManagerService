using MetricsAgent.Repositories.NetworkRepository;
using MetricsAgent.Models;
using Quartz;
using System.Diagnostics;

namespace MetricsAgent.Jobs;

public class NetworkMetricsJob : IJob
{
    private readonly INetworkRepository _repository;
    private readonly PerformanceCounter _netCounter;

    public NetworkMetricsJob(INetworkRepository repository)
    {
        _repository = repository;
        _netCounter = new PerformanceCounter("Network Interface",
            "Bytes Received/sec", 
            new PerformanceCounterCategory("Network Interface").GetInstanceNames()[0].ToString());
    }

    [STAThread]
    public Task Execute(IJobExecutionContext context)
    {
        _netCounter.NextValue();
        Thread.Sleep(200);
        var value = _netCounter.NextValue();
        _repository.Create(new NetworkMetrics()
        {
            Id = Guid.NewGuid(),
            Value = (int)value,
            Time = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds()).TotalSeconds
        });

        return Task.CompletedTask;
    }
}
