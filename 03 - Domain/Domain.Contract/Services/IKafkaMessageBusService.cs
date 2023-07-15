namespace Domain.Contract.Services;

public interface IKafkaMessageBusService
{
    void Publish(string topic, byte[] message);
    void Subscribe(string topic, Func<byte[], Task> messageHandler);
}
