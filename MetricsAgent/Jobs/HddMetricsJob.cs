using MetricsAgent.Repositories.HddRepository;
using Quartz;
using System.Diagnostics;

namespace MetricsAgent.Jobs;

public class HddMetricsJob: IJob
{
    private readonly IHddRepository _repository;
    //private readonly PerformanceCounter _cpuCounter;

    public HddMetricsJob(IHddRepository repository)
    {
        _repository = repository;
        //_cpuCounter = new PerformanceCounter("PhysicalDisk", "% Disk Time", "_Total");
    }
    [STAThread]
    public Task Execute(IJobExecutionContext context)
    {
        Thread.Sleep(150);

        DriveInfo[] drives = DriveInfo.GetDrives();
        var space = drives[0].TotalFreeSpace / (1024 * 1024 * 1024);
        _repository.Create(new Models.HddMetrics()
        {
            Id = Guid.NewGuid(),
            Value = (int)space,
            Time = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds()).TotalSeconds
        });
        
        return Task.CompletedTask;
    }
}
