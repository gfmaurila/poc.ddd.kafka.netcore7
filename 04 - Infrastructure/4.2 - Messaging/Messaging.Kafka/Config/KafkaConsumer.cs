using Domain.Contract.Services;

namespace Messaging.Kafka.Config;

public class KafkaConsumer
{
    private readonly IKafkaMessageBusService _messageBusService;

    public KafkaConsumer(IKafkaMessageBusService messageBusService)
    {
        _messageBusService = messageBusService;
    }

    public void Consume(string topic, Func<byte[], Task> messageHandler)
    {
        // Subscrever-se ao tópico Kafka e fornecer o manipulador de mensagem personalizado
        // Quando uma mensagem é recebida, o manipulador será chamado
        // O manipulador de mensagem deve ser uma função que aceita um array de bytes como argumento e retorna uma Task
        _messageBusService.Subscribe(topic, messageHandler);
    }
}

