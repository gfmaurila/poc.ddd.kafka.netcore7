namespace Domain.Contract.Producer;

public interface ICreateUserAllRabbitMQProducer
{
    void Publish();
}
