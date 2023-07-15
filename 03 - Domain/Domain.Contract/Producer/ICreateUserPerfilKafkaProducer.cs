using Application.DTOs;

namespace Domain.Contract.Producer;

public interface ICreateUserPerfilKafkaProducer
{
    void Publish(UserListDto dto);
}
