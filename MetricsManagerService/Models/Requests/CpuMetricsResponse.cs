using MetricsManagerService.Models.Dto;

namespace MetricsManagerService.Models.Requests
{
    public class CpuMetricsResponse
    {
        // Отображение id перенесено в модельметрики
        //public int AgentId { get; set; }  

        public CpuMetricsDto[]? CpuMetrics { get; set; }
    }
}
