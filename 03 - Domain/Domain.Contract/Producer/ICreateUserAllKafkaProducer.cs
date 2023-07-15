namespace Domain.Contract.Producer;

public interface ICreateUserAllKafkaProducer
{
    void Publish();
}
