using MetricsManagerService.Models.Requests;

namespace MetricsManagerService.Services
{
    public interface IMerticsAgentClient
    {
        CpuMetricsResponse GetCpuMetrics(CpuMetricsRequest request);
    }
}
