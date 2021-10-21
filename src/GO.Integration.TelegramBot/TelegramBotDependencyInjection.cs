using GO.Integration.TelegramBot.Abstractions;
using GO.Integration.TelegramBot.Behaviors.Factory;
using GO.Integration.TelegramBot.Behaviors.Management;
using GO.Integration.TelegramBot.Behaviors.Movie;
using GO.Integration.TelegramBot.Configurations;
using GO.Integration.TelegramBot.Constants;
using GO.Integration.TelegramBot.Logging;
using GO.Integration.TelegramBot.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Configuration;
using Telegram.Bot;

namespace GO.Integration.TelegramBot
{
    public static class TelegramBotConfigurations
    {
        public static void RegisterTelegramBot(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.Configure<TelegramBotConfiguration>(
                configuration.GetRequiredSection(nameof(TelegramBotConfiguration)));

            services.AddTelegramServices();

            var token = configuration
                .GetRequiredSection($"{nameof(TelegramBotConfiguration)}:{nameof(TelegramBotConfiguration.Token)}")
                .Get<string>();

            services.AddHttpClient(TelegramConstants.HttpClientName)
                .AddTypedClient<ITelegramBotClient>(httpClient
                    => new TelegramBotClient(token, httpClient));

            services.AddHostedService<WebhookSubscribeService>();
        }

        public static ILoggingBuilder AddTelegramBotLogger(
            this ILoggingBuilder builder,
            Action<TelegramBotLoggerConfiguration>? configure = null)
        {
            builder.AddTelegramBotLogger();

            if (configure != null)
            {
                builder.Services.Configure(configure);
            }

            return builder;
        }

        private static void AddTelegramServices(this IServiceCollection services)
        {
            services.AddTransient<IManagementBotBehavior, ManagementBotBehavior>();
            services.AddTransient<IMovieBotBehavior, MovieBotBehavior>();

            services.AddTransient<IBotBehaviorFactory, BotBehaviorFactory>();
            services.AddTransient<ITelegramBotListenerService, TelegramBotListenerService>();
            services.AddTransient<ITelegramBotClientService, TelegramBotClientService>();
        }

        private static ILoggingBuilder AddTelegramBotLogger(
            this ILoggingBuilder builder)
        {
            builder.Services.TryAddEnumerable(
                ServiceDescriptor.Singleton<ILoggerProvider, TelegramBotLoggerProvider>());

            LoggerProviderOptions.RegisterProviderOptions
                <TelegramBotLoggerConfiguration, TelegramBotLoggerProvider>(builder.Services);

            return builder;
        }
    }
}
