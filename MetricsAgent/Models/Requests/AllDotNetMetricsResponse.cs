using MetricsAgent.Models.Dto;

namespace MetricsAgent.Models.Requests;

public class AllDotNetMetricsResponse
{
    public List<DotNetMetricsDto> DotNetMetrics { get; set; }
}

