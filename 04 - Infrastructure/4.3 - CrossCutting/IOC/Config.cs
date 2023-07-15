using Application.Services;
using Application.Services.Event;
using Application.Validators;
using Confluent.Kafka;
using Data.Repository;
using Data.Repository.MongoDb;
using Data.Repository.Redis;
using Data.Repository.Repositories;
using Data.Repository.Repositories.EF;
using Data.SQLServer.Config;
using Domain.Contract.MongoDb;
using Domain.Contract.Producer;
using Domain.Contract.Redis;
using Domain.Contract.Repositories;
using Domain.Contract.Services;
using Domain.Contract.Services.Event;
using Messaging.Kafka.Config;
using Messaging.Kafka.Services.Producer;
using Messaging.RabbitMQ.Config;
using Messaging.RabbitMQ.Services.Consumers;
using Messaging.RabbitMQ.Services.Producer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using StackExchange.Redis;

namespace IOC;
public class Config
{
    public static void ConfigDbContext(IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<SQLServerContext>(options => options.UseSqlServer(configuration.GetConnectionString("SqlConnection")));
    }

    public static void ConfigMongoDb(IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IMongoClient>(new MongoClient(configuration.GetConnectionString("MongoDB")));
    }

    public static void ConfigRedis(IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(configuration.GetConnectionString("Redis")));

        services.AddScoped<IDatabase>(x =>
        {
            var multiplexer = x.GetRequiredService<IConnectionMultiplexer>();
            return multiplexer.GetDatabase();
        });

        services.AddScoped<ICacheRepository, CacheRepository>();
    }

    public static void ConfigRepository(IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
        services.AddScoped<IUserRepository, UserEFRepository>();
        services.AddSingleton<ILogRepository, LogRepository>();
    }

    public static void ConfigService(IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IUserKafkaService, UserKafkaService>();
        services.AddScoped<IUserRedisService, UserRedisService>();
    }

    public static void ConfigEvent(IServiceCollection services)
    {
        services.AddScoped<IErrorEvent, BadRequestEvent>();
        services.AddScoped<IErrorEvent, InternalServerErrorEvent>();
        services.AddScoped<IErrorEvent, NotFoundEvent>();
    }

    public static void ConfigValidator(IServiceCollection services)
    {
        services.AddScoped<CreateUserValidator>();
    }




    public static void ConfigBusService(IServiceCollection services)
    {
        // RabbitMQ
        services.AddScoped<IMessageRabbitMQBusService, MessageRabbitMQBusService>();
        services.AddScoped<ICreateUserAllRabbitMQProducer, CreateUserAllRabbitMQProducer>();
        services.AddScoped<ICreateUserRabbitMQProducer, CreateUserRabbitMQProducer>();
        services.AddScoped<IDeleteUserRabbitMQProducer, DeleteUserRabbitMQProducer>();
        services.AddScoped<ICreateUserPerfilRabbitMQProducer, CreateUserPerfilRabbitMQProducer>();

        services.AddHostedService<CreateUserAllRabbitMQConsumer>();
        services.AddHostedService<CreateUserRabbitMQConsumer>();
        services.AddHostedService<DeleteUserRabbitMQConsumer>();
        services.AddHostedService<CreateUserPerfilRabbitMQConsumer>();
    }

    public static void ConfigBusKafkaService(IServiceCollection services)
    {
        // Kafka
        services.AddSingleton<IConsumer<Ignore, string>>(sp =>
        {
            var consumerConfig = new ConsumerConfig
            {
                BootstrapServers = "localhost:9092",
                GroupId = "user-work-group",
                AutoOffsetReset = AutoOffsetReset.Earliest
                // Outras configurações
            };

            return new ConsumerBuilder<Ignore, string>(consumerConfig).Build();
        });

        services.AddSingleton<IKafkaMessageBusService, KafkaMessageBusService>();
        services.AddScoped<ICreateUserAllKafkaProducer, CreateUserAllKafkaProducer>();
        services.AddScoped<ICreateUserKafkaProducer, CreateUserKafkaProducer>();
        services.AddScoped<ICreateUserPerfilKafkaProducer, CreateUserPerfilKafkaProducer>();
        services.AddScoped<IDeleteUserKafkaProducer, DeleteUserKafkaProducer>();

        //services.AddScoped<CreateUserAllKafkaConsumer>();
        services.AddScoped<CreateUserKafkaConsumer>();
        //services.AddScoped<CreateUserPerfilKafkaConsumer>();
        //services.AddScoped<DeleteUserKafkaConsumer>();
    }
}
