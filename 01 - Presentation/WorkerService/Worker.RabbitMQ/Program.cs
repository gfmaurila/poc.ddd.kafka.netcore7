using Serilog;

namespace Worker.RabbitMQ
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
                    //services.AddHostedService<Worker2>();
                })
                .Build();

            host.Run();
        }
    }
}