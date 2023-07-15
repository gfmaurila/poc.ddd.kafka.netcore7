using Serilog;

namespace Worker.Kafka
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IHost host = Host.CreateDefaultBuilder(args)
                .UseSerilog((hostingContext, loggerConfiguration) =>
                {
                    loggerConfiguration
                        .ReadFrom.Configuration(hostingContext.Configuration)
                        .Enrich.FromLogContext();
                })
                .ConfigureServices(services =>
                {
                    services.AddSingleton(FallbackContagem.CreateFallbackPolicy());
                    services.AddHostedService<Worker>();
                })
                .Build();

            host.Run();
        }
    }
}