namespace MetricsAgent.Models.Requests;

public class RamMetricsCreateReqest
{
    public int Value { get; set; }
    public TimeSpan Time { get; set; }
}
