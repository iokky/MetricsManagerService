using MetricsManagerService.Models;
using MetricsManagerService.Repositories;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace MetricsManagerService.Controllers;




[Route("api/agents")]
[ApiController]

[SwaggerTag("Предоставляет метода для работы с агентами")]
public class AgentsController : ControllerBase
{
    private readonly IAgentRepository _repository;
    public AgentsController(IAgentRepository repository)
    {
        _repository = repository;
    }

    /// <summary>
    /// Получение всех записей агентов
    /// </summary>
    /// <returns>Все существующие записи</returns>
    [HttpGet("all")]
    public IActionResult GetAll() => Ok(_repository.GetAll());

    /// <summary>
    /// Регистарция нового агента
    /// </summary>
    /// <param name="agent"></param>
    /// <returns>ОК</returns>
    [HttpPost("add")]
    [SwaggerOperation(description: "Создание новой записи для агента")]
    [SwaggerResponse(200, "Ok")]
    [SwaggerResponse(400, "Bad Request")]
    public IActionResult AgentRegister([FromBody]Agent agent)
    {
        _repository.Add(agent);
         return Ok();
    }

    /// <summary>
    /// Изменение статуса агента off/on
    /// </summary>
    /// <param name="agentId"></param>
    /// <returns>Ok</returns>
    [HttpPut("switch/{agentId}")]
    public IActionResult SwitnAgentById([FromRoute] int agentId)
    {
        _repository.SwitchState(agentId);
        return Ok();
    }
}
