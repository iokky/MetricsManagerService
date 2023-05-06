namespace MetricsManagerService.Models
{
    public class HddMetrics
    {
        public Guid Id { get; set; }
        public int Value { get; set; }
        public double Time { get; set; }
        public int AgentId { get; set; }
    }
}
