using Polly;
using Polly.Fallback;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Worker.Kafka.Models;

namespace Worker.Kafka;

public static class FallbackContagem
{
    public static AsyncFallbackPolicy<ResultadoContador> CreateFallbackPolicy()
    {
        return Policy<ResultadoContador>
            .Handle<HttpRequestException>(ex => ex.StatusCode == HttpStatusCode.TooManyRequests)
            .FallbackAsync<ResultadoContador>(
                fallbackAction: (_, _) =>
                {
                    Console.Out.WriteLineAsync();
                    PrintStatusFallback(
                        "Worker.Kafka - Too Many Requests = 429 | Gerando o valor alternativo via Policy de Fallback (fallbackAction)...");
                    Console.Out.WriteLineAsync();

                    return Task.FromResult(new ResultadoContador()
                    {
                        ValorAtual = -1,
                        Mensagem = "Worker.Kafka - Valor gerado na Policy de Fallback"
                    });
                },
                onFallbackAsync: (responseToFailedRequest, _) =>
                {
                    Console.Out.WriteLineAsync();
                    PrintStatusFallback(
                        "Worker.Kafka - Iniciando a execução da Policy de Fallback (onFallbackAsync) | " +
                       $"Worker.Kafka - Descrição da falha: {responseToFailedRequest.Exception.Message}");
                    return Task.CompletedTask;
                });
    }

    private static void PrintStatusFallback(string status)
    {
        var previousForegroundColor = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Out.WriteLineAsync($"Worker.Kafka -  ***** {status} **** ");
        Console.ForegroundColor = previousForegroundColor;
    }
}
