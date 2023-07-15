using Application.DTOs;
using Domain.Contract.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace WebAPI.Controllers;

[Route("api/redis")]
[ApiController]
public class RedisController : ControllerBase
{
    private readonly IUserRedisService _service;

    public RedisController(IUserRedisService service)
    {
        _service = service;
    }

    /// <summary>
    /// Cria um novo usuário no Redis.
    /// </summary>
    /// <param name="request">Dados do usuário a serem criados.</param>
    /// <returns>Objeto criado.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(WorkerUserDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Create([FromBody] WorkerUserDto request)
    {
        var result = await _service.Create(request);
        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }
        else
        {
            return BadRequest();
        }
    }

    /// <summary>
    /// Cria todos os usuários no Redis.
    /// </summary>
    /// <returns>Resposta OK.</returns>
    [HttpGet("all")]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> CreateAll()
    {
        await _service.CreateRedis();
        return Ok("OK");
    }
}

