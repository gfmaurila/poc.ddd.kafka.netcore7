using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

public class CreateUserKafkaConsumer : BackgroundService
{
    private readonly ILogger<CreateUserKafkaConsumer> _logger;
    private readonly IConsumer<Ignore, string> _consumer;
    private const string TOPIC_NAME = "CREATE_USER";

    public CreateUserKafkaConsumer(ILogger<CreateUserKafkaConsumer> logger, IConsumer<Ignore, string> consumer)
    {
        _logger = logger;
        _consumer = consumer;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _consumer.Subscribe(TOPIC_NAME);

        try
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var consumeResult = _consumer.Consume(stoppingToken);
                _logger.LogInformation($"Received message: {consumeResult.Message.Value}");

                // Adicione sua lógica de processamento da mensagem aqui

                await Task.Delay(1000, stoppingToken); // Aguarda 1 segundo antes de consumir a próxima mensagem
            }
        }
        catch (OperationCanceledException)
        {
            // A tarefa foi cancelada, saia do loop de consumo
            _logger.LogInformation("Task was canceled, exiting consumer loop");
        }
        catch (ConsumeException ex)
        {
            _logger.LogError($"Failed to consume message: {ex.Error.Reason}");
        }
        finally
        {
            _consumer.Close();
        }
    }

    public override void Dispose()
    {
        _consumer.Dispose();
        base.Dispose();
    }
}



//using Application.DTOs;
//using Confluent.Kafka;
//using Domain.Contract.Services;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Hosting;
//using Newtonsoft.Json;
//using System.Text;

//namespace Messaging.Kafka.Services.Consumers;

//public class CreateUserKafkaConsumer : BackgroundService
//{
//    private readonly IKafkaMessageBusService _messageBusService;
//    private readonly IServiceProvider _serviceProvider;
//    private const string TOPIC_NAME = "CREATE_USER";

//    public CreateUserKafkaConsumer(IServiceProvider serviceProvider, IKafkaMessageBusService messageBusService)
//    {
//        _serviceProvider = serviceProvider;
//        _messageBusService = messageBusService;
//    }

//    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
//    {
//        var consumerConfig = new ConsumerConfig
//        {
//            BootstrapServers = "localhost:9092",
//            GroupId = "create-user-consumer-group",
//            AutoOffsetReset = AutoOffsetReset.Earliest
//            // Adicione outras configurações conforme necessário
//        };

//        using (var consumer = new ConsumerBuilder<Ignore, string>(consumerConfig).Build())
//        {
//            consumer.Subscribe(TOPIC_NAME);

//            try
//            {
//                while (!stoppingToken.IsCancellationRequested)
//                {
//                    var consumeResult = consumer.Consume(stoppingToken);

//                    if (consumeResult != null && consumeResult.Message.Value != null)
//                    {
//                        var message = Encoding.UTF8.GetBytes(consumeResult.Message.Value);
//                        await CreateUserRedis(message);
//                    }
//                }
//            }
//            catch (OperationCanceledException)
//            {
//                // A tarefa foi cancelada, saia do loop de consumo
//            }
//            catch (ConsumeException ex)
//            {
//                Console.WriteLine($"Failed to consume message: {ex.Error.Reason}");
//            }
//            finally
//            {
//                consumer.Close();
//            }
//        }
//    }

//    public async Task CreateUserRedis(byte[] message)
//    {
//        var infoJson = Encoding.UTF8.GetString(message);
//        var info = JsonConvert.DeserializeObject<UserListDto>(infoJson);

//        using (var scope = _serviceProvider.CreateScope())
//        {
//            var createUserRedis = scope.ServiceProvider.GetRequiredService<IUserRedisService>();
//            await createUserRedis.CreateRedis($"user_id_{info.Id}", info, 0);
//        }
//    }
//}


