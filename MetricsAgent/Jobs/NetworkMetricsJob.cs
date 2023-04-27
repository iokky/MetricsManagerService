using MetricsAgent.Repositories.CpuRepository;
using MetricsAgent.Repositories.NetworkRepository;
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
        Thread.Sleep(100);
        var value = _netCounter.NextValue();
        _repository.Create(new Models.NetworkMetrics()
        {
            Value = (int)value,
            Time = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds()).TotalSeconds
        });

        //Debug.WriteLine($"|NET| {TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds())}| {(int)_netCounter.NextValue()} byte");

        return Task.CompletedTask;
    }
}
