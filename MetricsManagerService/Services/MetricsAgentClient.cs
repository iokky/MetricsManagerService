using MetricsAgent.Logger;
using MetricsManagerService.Models.Requests;
using MetricsManagerService.Repositories;
using Newtonsoft.Json;

namespace MetricsManagerService.Services
{
    public class MetricsAgentClient : IMerticsAgentClient
    {
        private readonly IManagerLogger _logger;
        private readonly HttpClient _httpClient;
        private readonly IAgentRepository _repository;
        public MetricsAgentClient(
            HttpClient client,
            IAgentRepository repository,
            IManagerLogger logger) 
        {
            _logger = logger;
            _httpClient = client;
            _repository = repository;
        }
        public CpuMetricsResponse GetCpuMetrics(CpuMetricsRequest cpuRequest)
        {
            try
            {
                //TODO: Проверка if agent => enable true
                string AgentUri = _repository.GetById(cpuRequest.AgentId).AgentAddress.ToString();
                string uri = $"{AgentUri}api/cpu/from/{cpuRequest.FromTime}/to/{cpuRequest.ToTime}";

                HttpRequestMessage request = new(HttpMethod.Get, uri);
                request.Headers.Add("Accept", "application/json");
                HttpResponseMessage response = _httpClient.SendAsync(request).Result;


                if (response.IsSuccessStatusCode)
                {
                    var arr = response.Content.ReadAsStringAsync().Result;
                    CpuMetricsResponse? metrics = JsonConvert.DeserializeObject<CpuMetricsResponse>(arr);
                    metrics.AgentId = cpuRequest.AgentId;
                    return metrics;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }
    }
}
