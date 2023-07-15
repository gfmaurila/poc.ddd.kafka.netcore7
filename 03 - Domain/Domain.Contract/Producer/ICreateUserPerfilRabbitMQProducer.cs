using Application.DTOs;

namespace Domain.Contract.Producer;

public interface ICreateUserPerfilRabbitMQProducer
{
    void Publish(UserListDto dto);
}
