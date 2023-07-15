using Confluent.Kafka;
using Domain.Contract.Services;

namespace Messaging.Kafka.Config;

public class KafkaMessageBusService : IKafkaMessageBusService
{
    private readonly ProducerConfig _producerConfig;
    private readonly ConsumerConfig _consumerConfig;

    public KafkaMessageBusService()
    {
        _producerConfig = new ProducerConfig
        {
            BootstrapServers = "localhost:9092",
            ClientId = Guid.NewGuid().ToString()
        };

        _consumerConfig = new ConsumerConfig
        {
            BootstrapServers = "localhost:9092",
            GroupId = "user-work-group",
            AutoOffsetReset = AutoOffsetReset.Earliest
        };
    }

    public void Publish(string topic, byte[] message)
    {
        using (var producer = new ProducerBuilder<Null, byte[]>(_producerConfig).Build())
        {
            var kafkaMessage = new Message<Null, byte[]>
            {
                Key = null,
                Value = message
            };

            try
            {
                var deliveryResult = producer.ProduceAsync(topic, kafkaMessage).GetAwaiter().GetResult();
                Console.WriteLine($"Mensagem enviada para o tópico: {deliveryResult.Topic} | Partição: {deliveryResult.Partition} | Offset: {deliveryResult.Offset}");
            }
            catch (ProduceException<Null, byte[]> ex)
            {
                Console.WriteLine($"Falha ao enviar a mensagem: {ex.Error.Reason}");
            }
        }
    }

    public void Subscribe(string topic, Func<byte[], Task> messageHandler)
    {
        using (var consumer = new ConsumerBuilder<Ignore, byte[]>(_consumerConfig).Build())
        {
            consumer.Subscribe(topic);

            try
            {
                while (true)
                {
                    var consumeResult = consumer.Consume();

                    if (consumeResult != null && consumeResult.Message.Value != null)
                    {
                        messageHandler(consumeResult.Message.Value).GetAwaiter().GetResult();
                    }
                }
            }
            catch (ConsumeException ex)
            {
                Console.WriteLine($"Falha ao consumir mensagem: {ex.Error.Reason}");
            }
            finally
            {
                consumer.Close();
            }
        }
    }
}

