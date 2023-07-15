using Domain.Contract.Producer;
using Domain.Contract.Services;
using System.Text;

namespace Messaging.Kafka.Services.Producer;

public class CreateUserAllKafkaProducer : ICreateUserAllKafkaProducer
{
    private readonly IKafkaMessageBusService _messageBusService;
    private const string TOPIC_NAME = "CREATE_ALL_USER";

    public CreateUserAllKafkaProducer(IKafkaMessageBusService messageBusService)
    {
        _messageBusService = messageBusService;
    }

    public void Publish()
    {
        var message = Encoding.UTF8.GetBytes("");

        _messageBusService.Publish(TOPIC_NAME, message);
    }
}
