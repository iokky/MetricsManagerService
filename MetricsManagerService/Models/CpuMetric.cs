namespace MetricsManagerService.Models
{
    public class CpuMetric
    {
        public Guid Id { get; set; }
        public TimeSpan Time { get; set; }
        public int Value { get; set; }
    }
}
