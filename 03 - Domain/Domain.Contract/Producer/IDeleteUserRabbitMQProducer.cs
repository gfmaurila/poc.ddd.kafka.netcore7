using Application.DTOs;

namespace Domain.Contract.Producer;

public interface IDeleteUserRabbitMQProducer
{
    void Publish(UserListDto dto);
}
