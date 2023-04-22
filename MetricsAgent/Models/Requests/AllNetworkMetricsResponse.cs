using MetricsAgent.Models.Dto;

namespace MetricsAgent.Models.Requests
{
    public class AllNetworkMetricsResponse
    {
        public List<NetworkMetricsDto> NetworkMetrics { get; set; }
    }
}
