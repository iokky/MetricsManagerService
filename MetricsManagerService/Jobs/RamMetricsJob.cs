using MetricsManagerService.Logger;
using MetricsManagerService.Repositories.Network;
using MetricsManagerService.Repositories;
using MetricsManagerService.Services;
using Quartz;
using Quartz.Spi;
using System.Diagnostics;
using MetricsManagerService.Repositories.Ram;
using MetricsManagerService.Models.Requests;
using MetricsManagerService.Models;

namespace MetricsManagerService.Jobs;

public class RamMetricsJob : IJob
{
    private readonly IRamMetricsRepository _ramRepository;
    private readonly IAgentRepository _agentRepository;
    private readonly IMerticsAgentClient _httpClient;
    private readonly IManagerLogger _logger;

    public RamMetricsJob(
        IRamMetricsRepository repository,
        IAgentRepository agentRepository,
        IMerticsAgentClient client,
        IManagerLogger logger)
    {
        _ramRepository = repository;
        _agentRepository = agentRepository;
        _httpClient = client;
        _logger = logger;
    }
    [STAThread]
    public Task Execute(IJobExecutionContext context)
    {

        Thread.Sleep(200);
        var data = _agentRepository.Agents;
        foreach (var item in data)
        {
            var time = _ramRepository.GetLastTime();
            var response = _httpClient.GetRamMetrics(new RamMetricsRequest()
            {
                AgentId = item.AgentId,
                FromTime = TimeSpan.FromSeconds(_ramRepository.GetLastTime()),
                ToTime = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds())
            }, item.AgentAddress); ;
            if (response != null && response.RamMetrics != null)
            {
                foreach (var metric in response.RamMetrics)
                {
                    try
                    {
                        _ramRepository.Create(new RamMetrics()
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
