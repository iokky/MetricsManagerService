

namespace MetricsAgent.Models.Dto;


//DTO - Data Transfer Object
public class CpuMetricsDto
{
    public int Id { get; set; }
    public int Value { get; set; }
    public TimeSpan Time { get; set; }

}
