using Application.DTOs;

namespace Domain.Contract.Producer;

public interface ICreateUserKafkaProducer
{
    void Publish(UserListDto dto);
}
