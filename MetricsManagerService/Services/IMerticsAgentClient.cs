using MetricsManagerService.Models.Requests;

namespace MetricsManagerService.Services
{
    public interface IMerticsAgentClient
    {
        CpuMetricsResponse GetCpuMetrics(CpuMetricsRequest request, Uri AgentUri);
        HddMetricsResponse GetHddMetrics(HddMetricsRequest request, Uri AgentUri);
        NetworkMetricsResponse GetNetworkMetrics(NetworkMetricsRequest request, Uri AgentUri);
        RamMetricsResponse GetRamMetrics(RamMetricsRequest request, Uri AgentUri);
    }
}
