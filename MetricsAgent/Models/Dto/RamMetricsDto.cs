namespace MetricsAgent.Models.Dto;

public class RamMetricsDto
{
    public Guid Id { get; set; }
    public int Value { get; set; }
    public TimeSpan Time { get; set; }
}
