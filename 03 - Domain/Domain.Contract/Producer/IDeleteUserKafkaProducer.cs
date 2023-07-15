using Application.DTOs;

namespace Domain.Contract.Producer;

public interface IDeleteUserKafkaProducer
{
    void Publish(UserListDto dto);
}
