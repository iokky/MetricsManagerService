namespace MetricsManagerService.Models.Requests
{
    public class CpuMetricsResponse
    {
        public int AgentId { get; set; }  
        public CpuMetric[]? CpuMetrics { get; set; }
    }
}
