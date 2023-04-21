namespace MetricsAgent.Models.Requests
{
    public class CpuMetricsCreateRequest
    {
        public int Value { get; set; }
        public TimeSpan Time { get; set; }
    }
}
