using GO.Integration.TelegramBot;

namespace GO.Core.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
                Host.CreateDefaultBuilder(args)
                        .ConfigureLogging((context, builder) =>
                        {
                            builder.ClearProviders()
                                .AddConfiguration(context.Configuration.GetSection("Logging"))
                                .AddConsole()
                                .AddDebug()
                                .AddTelegramBotLogger();
                        })
                        .ConfigureWebHostDefaults(webBuilder =>
                        {
                            webBuilder.UseStartup<Startup>();
                        });
    }
}
