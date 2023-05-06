using MetricsManagerService.Logger;
using MetricsManagerService.Models.Requests;
using MetricsManagerService.Repositories;
using Newtonsoft.Json;

namespace MetricsManagerService.Services
{
    public class MetricsAgentClient : IMerticsAgentClient
    {
        private readonly IManagerLogger _logger;
        private readonly HttpClient _httpClient;
        public MetricsAgentClient(
            HttpClient client,
            IManagerLogger logger) 
        {
            _logger = logger;
            _httpClient = client;
        }

        #region CpuAgentRequest
        public CpuMetricsResponse GetCpuMetrics(CpuMetricsRequest cpuReq, Uri AgentUri)
        {
            try
            {
                //TODO: Проверка if agent => enable true
                string uri = $"{AgentUri}api/cpu/from/{cpuReq.FromTime}/to/{cpuReq.ToTime}";

                HttpRequestMessage request = new(HttpMethod.Get, uri);
                request.Headers.Add("Accept", "application/json");
                HttpResponseMessage response = _httpClient.SendAsync(request).Result;


                if (response.IsSuccessStatusCode)
                {
                    var arr = response.Content.ReadAsStringAsync().Result;
                    CpuMetricsResponse? metrics = JsonConvert.DeserializeObject<CpuMetricsResponse>(arr); 
                        
                    return metrics;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }
        #endregion

        #region HddAgentRequest
        public HddMetricsResponse GetHddMetrics(HddMetricsRequest hddReq, Uri AgentUri)
        {
            try
            {
                //TODO: Проверка if agent => enable true
                string uri = $"{AgentUri}api/hdd/left/from/{hddReq.FromTime}/to/{hddReq.ToTime}";

                HttpRequestMessage request = new(HttpMethod.Get, uri);
                request.Headers.Add("Accept", "application/json");
                HttpResponseMessage response = _httpClient.SendAsync(request).Result;


                if (response.IsSuccessStatusCode)
                {
                    var arr = response.Content.ReadAsStringAsync().Result;
                    HddMetricsResponse? metrics = JsonConvert.DeserializeObject<HddMetricsResponse>(arr);

                    return metrics;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }
        #endregion
        public NetworkMetricsResponse GetNetworkMetrics(NetworkMetricsRequest netReq, Uri AgentUri)
        {
            try
            {
                //TODO: Проверка if agent => enable true
                string uri = $"{AgentUri}api/network/from/{netReq.FromTime}/to/{netReq.ToTime}";

                HttpRequestMessage request = new(HttpMethod.Get, uri);
                request.Headers.Add("Accept", "application/json");
                HttpResponseMessage response = _httpClient.SendAsync(request).Result;


                if (response.IsSuccessStatusCode)
                {
                    var arr = response.Content.ReadAsStringAsync().Result;
                    NetworkMetricsResponse? metrics = JsonConvert.DeserializeObject<NetworkMetricsResponse>(arr);

                    return metrics;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }

        public RamMetricsResponse GetRamMetrics(RamMetricsRequest ramReq, Uri AgentUri)
        {
            try
            {
                //TODO: Проверка if agent => enable true
                string uri = $"{AgentUri}api/ram/available/from/{ramReq.FromTime}/to/{ramReq.ToTime}";

                HttpRequestMessage request = new(HttpMethod.Get, uri);
                request.Headers.Add("Accept", "application/json");
                HttpResponseMessage response = _httpClient.SendAsync(request).Result;


                if (response.IsSuccessStatusCode)
                {
                    var arr = response.Content.ReadAsStringAsync().Result;
                    RamMetricsResponse? metrics = JsonConvert.DeserializeObject<RamMetricsResponse>(arr);

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
