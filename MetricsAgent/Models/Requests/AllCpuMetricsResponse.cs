using MetricsAgent.Models.Dto;

namespace MetricsAgent.Models.Requests;

public class AllCpuMetricsResponse
{
    public List<CpuMetricsDto> CpuMetrics { get; set; }
}
