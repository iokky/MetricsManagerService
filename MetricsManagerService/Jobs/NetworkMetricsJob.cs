using MetricsManagerService.Logger;
using MetricsManagerService.Repositories;
using MetricsManagerService.Services;
using Quartz;
using MetricsManagerService.Repositories.Network;
using MetricsManagerService.Models.Requests;
using MetricsManagerService.Models;

namespace MetricsManagerService.Jobs;

public class NetworkMetricsJob : IJob
{

    private readonly INetworkMetricsRepository _networkRepository;
    private readonly IAgentRepository _agentRepository;
    private readonly IMerticsAgentClient _httpClient;
    private readonly IManagerLogger _logger;

    public NetworkMetricsJob(
        INetworkMetricsRepository repository,
        IAgentRepository agentRepository,
        IMerticsAgentClient client,
        IManagerLogger logger)
    {
        _networkRepository = repository;
        _agentRepository = agentRepository;
        _httpClient = client;
        _logger = logger;
    }

    [STAThread]
    public Task Execute(IJobExecutionContext context)
    {
        Thread.Sleep(150);
        var data = _agentRepository.Agents;
        foreach (var item in data)
        {
            var time = _networkRepository.GetLastTime();
            var response = _httpClient.GetNetworkMetrics(new NetworkMetricsRequest()
            {
                AgentId = item.AgentId,
                FromTime = TimeSpan.FromSeconds(_networkRepository.GetLastTime()),
                ToTime = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds())
            }, item.AgentAddress); ;
            if (response != null && response.NetworkMetrics != null)
            {
                foreach (var metric in response.NetworkMetrics)
                {
                    try
                    {
                        _networkRepository.Create(new NetworkMetrics()
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
