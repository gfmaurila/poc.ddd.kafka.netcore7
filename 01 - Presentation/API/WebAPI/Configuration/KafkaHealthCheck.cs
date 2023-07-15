using Confluent.Kafka;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace WebAPI.Configuration;

public class KafkaHealthCheck : IHealthCheck
{
    private readonly ProducerConfig _config;

    public KafkaHealthCheck(string bootstrapServers)
    {
        _config = new ProducerConfig { BootstrapServers = bootstrapServers };
    }

    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default(CancellationToken))
    {
        using (var producer = new ProducerBuilder<Null, string>(_config).Build())
        {
            try
            {
                await producer.ProduceAsync("health_check", new Message<Null, string> { Value = "Health check message" }, cancellationToken);
                return HealthCheckResult.Healthy();
            }
            catch
            {
                return HealthCheckResult.Unhealthy();
            }
        }
    }
}