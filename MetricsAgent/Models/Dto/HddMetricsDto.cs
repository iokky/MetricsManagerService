namespace MetricsAgent.Models.Dto;

public class HddMetricsDto
{
    public int Guid { get; set; }
    public int Value { get; set; }
    public TimeSpan Time { get; set; }
}
