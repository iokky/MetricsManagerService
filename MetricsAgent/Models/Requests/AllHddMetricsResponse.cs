using MetricsAgent.Models.Dto;

namespace MetricsAgent.Models.Requests;

public class AllHddMetricsResponse
{
    public List<HddMetricsDto> HddMetrics { get; set; }
}
