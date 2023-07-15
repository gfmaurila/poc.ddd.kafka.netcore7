using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

public class CreateUserAllKafkaConsumer : BackgroundService
{
    private readonly ILogger<CreateUserAllKafkaConsumer> _logger;
    private readonly IConsumer<Ignore, string> _consumer;
    private const string TOPIC_NAME = "CREATE_ALL_USER";

    public CreateUserAllKafkaConsumer(ILogger<CreateUserAllKafkaConsumer> logger, IConsumer<Ignore, string> consumer)
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

//using Domain.Contract.Services;
//using Messaging.Kafka.Config;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Hosting;

//namespace Messaging.Kafka.Services.Consumers;

//public class CreateUserAllKafkaConsumer : BackgroundService
//{
//    private readonly IKafkaMessageBusService _messageBusService;
//    private readonly IServiceProvider _serviceProvider;
//    private const string TOPIC_NAME = "CREATE_ALL_USER";

//    public CreateUserAllKafkaConsumer(IServiceProvider serviceProvider, IKafkaMessageBusService messageBusService)
//    {
//        _serviceProvider = serviceProvider;
//        _messageBusService = messageBusService;
//    }

//    protected override Task ExecuteAsync(CancellationToken stoppingToken)
//    {
//        var consumer = new KafkaConsumer(_messageBusService);
//        consumer.Consume(TOPIC_NAME, CreateUserRedis);
//        return Task.CompletedTask;
//    }

//    public async Task CreateUserRedis(byte[] message)
//    {
//        using (var scope = _serviceProvider.CreateScope())
//        {
//            var createUserRedis = scope.ServiceProvider.GetRequiredService<IUserRedisService>();
//            await createUserRedis.CreateRedis();
//        }
//    }
//}


