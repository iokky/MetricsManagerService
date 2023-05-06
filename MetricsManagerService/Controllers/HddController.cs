using AutoMapper;
using MetricsManagerService.Logger;
using MetricsManagerService.Models;
using MetricsManagerService.Models.Dto;
using MetricsManagerService.Models.Requests;
using MetricsManagerService.Repositories.Hdd;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MetricsManagerService.Controllers
{
    [Route("api/hdd")]
    [ApiController]
    public class HddController : ControllerBase
    {
        private readonly IHddMetricsRepository _hddRepository;
        private readonly IMapper _mapper;
        private readonly IManagerLogger _logger;
        public HddController(IHddMetricsRepository repository, IMapper mapper, IManagerLogger logger)
        {
            _hddRepository = repository;
            _logger = logger;
        }
        [HttpGet("agent/{agentId}/left/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAgent([FromRoute] int agentId, [FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            var response = new HddMetricsResponse()
            {
                HddMetrics = _hddRepository.GetByRange(agentId, fromTime, toTime).Select(i =>
                _mapper.Map<HddMetricsDto>(i)).ToArray(),

            };

            return Ok(response);
        }

        [HttpGet("cluster/left")]
        public IActionResult GetMetricsFromAllCluster([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            var response = new HddMetricsResponse()
            { 
                HddMetrics = _hddRepository.GetAllByRange(fromTime, toTime).Select(i =>
                _mapper.Map<HddMetricsDto>(i)).ToArray(),

            };

            return Ok(response);
        }
    }
}

