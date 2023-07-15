using Domain.Contract.Producer;
using Domain.Contract.Services;

namespace Messaging.RabbitMQ.Services.Producer;

public class CreateUserAllRabbitMQProducer : ICreateUserAllRabbitMQProducer
{
    private readonly IMessageRabbitMQBusService _messageBusService;
    private const string QUEUE_NAME = "CREATE_ALL_USER";
    public CreateUserAllRabbitMQProducer(IMessageRabbitMQBusService messageBusService)
    {
        _messageBusService = messageBusService;
    }

    public void Publish()
    {
        _messageBusService.Publish(QUEUE_NAME, null);
    }
}
