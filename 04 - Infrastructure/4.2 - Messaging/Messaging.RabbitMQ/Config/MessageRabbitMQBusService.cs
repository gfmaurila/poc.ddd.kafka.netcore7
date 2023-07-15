using Domain.Contract.Services;
using RabbitMQ.Client;

namespace Messaging.RabbitMQ.Config;

public class MessageRabbitMQBusService : IMessageRabbitMQBusService
{
    private readonly ConnectionFactory _connectionFactory;

    public MessageRabbitMQBusService()
    {
        _connectionFactory = new ConnectionFactory
        {
            HostName = "localhost",
        };
    }
    public void Publish(string queue, byte[] message)
    {
        using (var connection = _connectionFactory.CreateConnection())
        {
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(
                    queue: queue,
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

                channel.BasicPublish(
                    exchange: "",
                    routingKey: queue,
                    basicProperties: null,
                    body: message);
            }
        }
    }
}