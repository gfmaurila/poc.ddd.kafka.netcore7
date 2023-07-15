using Polly.Fallback;
using System.Net.Http.Json;
using Worker.RabbitMQ.Models;

namespace Worker.RabbitMQ;
public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IConfiguration _configuration;
    private readonly AsyncFallbackPolicy<ResultadoContador> _resiliencePolicy;

    public Worker(ILogger<Worker> logger,
        IConfiguration configuration,
        AsyncFallbackPolicy<ResultadoContador> resiliencePolicy)
    {
        _logger = logger;
        _configuration = configuration;
        _resiliencePolicy = resiliencePolicy;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var httpClient = new HttpClient();
        var urlApiContagem = _configuration["UrlApiContagem"];

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var resultado = await _resiliencePolicy.ExecuteAsync(() =>
                {
                    return httpClient.GetFromJsonAsync<ResultadoContador>(urlApiContagem)!;
                });

                Console.WriteLine($"* {DateTime.Now:HH:mm:ss} * " +
                    $"Worker.RabbitMQ - Contador Worker 1 = {resultado.ValorAtual} | " +
                    $"Worker.RabbitMQ - Mensagem Worker 1  = {resultado.Mensagem}");

                _logger.LogInformation($"* {DateTime.Now:HH:mm:ss} * " +
                    $"Worker.RabbitMQ - Contador Worker 1  = {resultado.ValorAtual} | " +
                    $"Worker.RabbitMQ - Mensagem Worker 1  = {resultado.Mensagem}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"# {DateTime.Now:HH:mm:ss} # " +
                    $"Worker.RabbitMQ - Falha ao invocar a API  Worker 1 : {ex.GetType().FullName} | {ex.Message}");

                _logger.LogError($"# {DateTime.Now:HH:mm:ss} # " +
                    $"Worker.RabbitMQ - Falha ao invocar a API  Worker 1 : {ex.GetType().FullName} | {ex.Message}");
            }

            await Task.Delay(1000, stoppingToken);
        }
    }
}