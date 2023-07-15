using Application.DTOs;
using Domain.Contract.Services;
using Messaging.Kafka.Config;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text;
using System.Text.Json;

namespace Messaging.Kafka.Services.Consumers;

public class DeleteUserKafkaConsumer : BackgroundService
{
    private readonly IKafkaMessageBusService _messageBusService;
    private readonly IServiceProvider _serviceProvider;
    private const string TOPIC_NAME = "DELETE_USER";

    public DeleteUserKafkaConsumer(IServiceProvider serviceProvider, IKafkaMessageBusService messageBusService)
    {
        _serviceProvider = serviceProvider;
        _messageBusService = messageBusService;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var consumer = new KafkaConsumer(_messageBusService);
        consumer.Consume(TOPIC_NAME, CreateUserRedis);
        return Task.CompletedTask;
    }

    public async Task CreateUserRedis(byte[] message)
    {
        var infoJson = Encoding.UTF8.GetString(message);
        var info = JsonSerializer.Deserialize<UserListDto>(infoJson);

        using (var scope = _serviceProvider.CreateScope())
        {
            var createUserRedis = scope.ServiceProvider.GetRequiredService<IUserRedisService>();
            await createUserRedis.CreateRedis($"user_delete_id_{info.Id}", info, 1);
        }
    }
}


