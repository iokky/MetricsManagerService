namespace MetricsAgent.Models.Dto;

public class DotNetMetricsDto
{
    public int Id { get; set; }
    public int Value { get; set; }
    public TimeSpan Time { get; set; }
}
