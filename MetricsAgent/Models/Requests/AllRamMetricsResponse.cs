using MetricsAgent.Models.Dto;

namespace MetricsAgent.Models.Requests;

public class AllRamMetricsResponse
{
    public List<RamMetricsDto> RamMetrics { get; set; }
}
