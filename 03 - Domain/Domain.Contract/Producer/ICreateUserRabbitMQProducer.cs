using Application.DTOs;

namespace Domain.Contract.Producer;

public interface ICreateUserRabbitMQProducer
{
    void Publish(UserListDto dto);
}
