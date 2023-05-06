using MetricsManagerService.Models.Dto;

namespace MetricsManagerService.Models.Requests
{
    public class NetworkMetricsResponse
    {
        public NetworkMetricsDto[]? NetworkMetrics { get; set; }
    }
}
