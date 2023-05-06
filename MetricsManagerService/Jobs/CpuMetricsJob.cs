using MetricsManagerService.Logger;
using MetricsManagerService.Models;
using MetricsManagerService.Models.Requests;
using MetricsManagerService.Repositories;
using MetricsManagerService.Repositories.CPU;
using MetricsManagerService.Services;
using Quartz;

namespace MetricsManagerService.Jobs;

public class CpuMetricsJob : IJob
{
    private readonly ICpuMetricsRepository _cpuRepository;
    private readonly IAgentRepository _agentRepository;
    private readonly IMerticsAgentClient _httpClient;
    private readonly IManagerLogger _logger;

    public CpuMetricsJob(
        ICpuMetricsRepository cpuRepository, 
        IAgentRepository agentRepository,
        IMerticsAgentClient httpClient,
        IManagerLogger logger)
    {
        _cpuRepository = cpuRepository;
        _agentRepository = agentRepository;
        _httpClient = httpClient;
        _logger = logger;
    }

    [STAThread]
    public Task Execute(IJobExecutionContext context)
    {
        var data = _agentRepository.Agents;
        foreach (var item in data)
        {
           var time = _cpuRepository.GetLastTime();           
           var response =  _httpClient.GetCpuMetrics(new CpuMetricsRequest()
           {
                AgentId = item.AgentId,
                FromTime = TimeSpan.FromSeconds(_cpuRepository.GetLastTime()),
                ToTime = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds())
            }, item.AgentAddress);
            if (response != null && response.CpuMetrics != null)
            {
                foreach (var metric in response.CpuMetrics)
                {
                    try
                    {
                        _cpuRepository.Create(new CpuMetrics()
                        {
                            AgentId = item.AgentId,
                            Value = metric.Value,
                            Time = metric.Time.TotalSeconds
                        });
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex.Message);
                    }
                }

            }

        }


        return Task.CompletedTask;
    }
}
