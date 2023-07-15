using Application.DTOs;
using Ardalis.Result;

namespace Domain.Contract.Services;

public interface IUserRedisService
{
    Task CreateRedis();
    Task CreateRedis(string key, UserListDto dto, int db);
    Task<Result<WorkerUserDto>> Create(WorkerUserDto dto);
}
