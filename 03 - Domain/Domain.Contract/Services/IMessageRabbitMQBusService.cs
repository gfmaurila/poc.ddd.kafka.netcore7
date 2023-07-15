namespace Domain.Contract.Services;

public interface IMessageRabbitMQBusService
{
    void Publish(string queue, byte[] message);
}
