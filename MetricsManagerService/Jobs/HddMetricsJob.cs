using MetricsManagerService.Logger;
using MetricsManagerService.Models;
using MetricsManagerService.Models.Requests;
using MetricsManagerService.Repositories;
using MetricsManagerService.Repositories.Hdd;
using MetricsManagerService.Services;
using Quartz;
using System.Diagnostics;

namespace MetricsManagerService.Jobs;

public class HddMetricsJob: IJob
{
    private readonly IHddMetricsRepository _hddRepository;
    private readonly IAgentRepository _agentRepository;
    private readonly IMerticsAgentClient _httpClient;
    private readonly IManagerLogger _logger;
    public HddMetricsJob(
        IHddMetricsRepository repository,
        IAgentRepository agentRepository,
        IMerticsAgentClient client,
        IManagerLogger logger)
    {
        _hddRepository = repository;
        _agentRepository = agentRepository;
        _httpClient = client;
        _logger = logger;

    }
    [STAThread]
    public Task Execute(IJobExecutionContext context)
    {
        Thread.Sleep(100);
        var data =  _agentRepository.Agents;
        foreach (var item in data)
        {
            var time = _hddRepository.GetLastTime();
            var response = _httpClient.GetHddMetrics(new HddMetricsRequest()
            {
                AgentId = item.AgentId,
                FromTime = TimeSpan.FromSeconds(_hddRepository.GetLastTime()),
                ToTime = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds())
            }, item.AgentAddress); ;
            if (response != null && response.HddMetrics != null)
            {
                foreach (var metric in response.HddMetrics)
                {
                    try
                    {
                        _hddRepository.Create(new HddMetrics()
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
