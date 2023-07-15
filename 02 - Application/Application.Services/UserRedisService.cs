using Application.DTOs;
using Ardalis.Result;
using AutoMapper;
using Domain.Contract.Redis;
using Domain.Contract.Repositories;
using Domain.Contract.Services;
using Microsoft.Extensions.Logging;

namespace Application.Services;
public class UserRedisService : IUserRedisService
{
    private readonly IMapper _mapper;
    private readonly IUserRepository _repo;
    private readonly ICacheRepository _repoCache;
    private readonly ILogger<UserRedisService> _logger;

    public UserRedisService(
                           IMapper mapper,
                           IUserRepository repo,
                           ICacheRepository repoCache,
                           ILogger<UserRedisService> logger)
    {
        _mapper = mapper;
        _repo = repo;
        _repoCache = repoCache;
        _logger = logger;
    }

    #region CreateRedis
    public async Task CreateRedis()
    {
        var users = await _repo.Get();
        foreach (var user in users)
        {
            var mapperdto = _mapper.Map<UserDto>(user);
            await _repoCache.CreateBatch($"user_id_{mapperdto.Id}", mapperdto, TimeSpan.FromHours(1), 0);
        }
    }

    public async Task CreateRedis(string key, UserListDto dto, int db)
    {
        await _repoCache.SetAsync(key, dto, db);
    }

    public async Task CreateRedis(WorkerUserDto dto)
    {
        await _repoCache.SetAsync(dto.Key, dto, dto.Db);
    }

    public async Task<Result<WorkerUserDto>> Create(WorkerUserDto dto)
    {
        try
        {
            var tempo = TimeSpan.FromDays(dto.Tempo);
            await _repoCache.SetAsync($"{dto.Key}_id_{dto.Id}", dto, tempo, dto.Db);
            return Result.Success(dto, "Cadastrado com sucesso!");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Erro - Redis - Usuario: {ex.Message}");
            return Result.NotFound($"Erro - Redis - Usuario");
        }

    }
    #endregion
}
