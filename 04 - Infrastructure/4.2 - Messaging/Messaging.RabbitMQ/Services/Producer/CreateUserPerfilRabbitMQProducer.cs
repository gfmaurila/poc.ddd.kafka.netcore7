using Application.DTOs;
using Domain.Contract.Producer;
using Domain.Contract.Services;
using System.Text;
using System.Text.Json;


namespace Messaging.RabbitMQ.Services.Producer;

public class CreateUserPerfilRabbitMQProducer : ICreateUserPerfilRabbitMQProducer
{
    private readonly IMessageRabbitMQBusService _messageBusService;
    private const string QUEUE_NAME = "CREATE_USER_PERFIL";
    public CreateUserPerfilRabbitMQProducer(IMessageRabbitMQBusService messageBusService)
    {
        _messageBusService = messageBusService;
    }

    public void Publish(UserListDto dto)
    {
        var info = new UserListDto(dto.Id, dto.FullName, dto.Email, dto.Phone, dto.BirthDate, dto.Modified, dto.Active, dto.Role);
        var infoJson = JsonSerializer.Serialize(info);
        var infoBytes = Encoding.UTF8.GetBytes(infoJson);
        _messageBusService.Publish(QUEUE_NAME, infoBytes);
    }
}
