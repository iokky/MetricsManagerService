namespace MetricsManagerService.Models
{
    public class NetworkMetrics
    {
        public Guid Id { get; set; }
        public int Value { get; set; }
        public double Time { get; set; }
        public int AgentId { get; set; }
    }
}
