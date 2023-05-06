using AutoMapper;
using MetricsManagerService.Logger;
using MetricsManagerService.Models.Dto;
using MetricsManagerService.Models.Requests;
using MetricsManagerService.Repositories.Network;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MetricsManagerService.Controllers
{
    [Route("api/network")]
    [ApiController]
    public class NetworkController : ControllerBase
    {
        private readonly INetworkMetricsRepository _networkRepository;
        private readonly IMapper _mapper;
        private readonly IManagerLogger _logger;
        public NetworkController(INetworkMetricsRepository repository, IMapper mapper, IManagerLogger logger)
        {
            _networkRepository = repository;
            _mapper = mapper;
            _logger = logger;
                
        }
        [HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAgent([FromRoute] int agentId, [FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime) 
        {
            var response = new NetworkMetricsResponse()
            {
                NetworkMetrics = _networkRepository.GetByRange(agentId, fromTime, toTime).Select(i =>
                _mapper.Map<NetworkMetricsDto>(i)).ToArray(),

            };

            return Ok(response);
        }

        [HttpGet("cluster/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAllCluster([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime) 
        {
            var response = new NetworkMetricsResponse()
            {
                NetworkMetrics = _networkRepository.GetAllByRange(fromTime, toTime).Select(i =>
                _mapper.Map<NetworkMetricsDto>(i)).ToArray(),

            };

            return Ok(response);
        }
    }
}
