namespace MetricsManagerService.Models.Dto
{
    public class RamMetricsDto
    {
        public Guid Id { get; set; }
        public int Value { get; set; }
        public TimeSpan Time { get; set; }
        public int AgentId { get; set; }
    }
}
