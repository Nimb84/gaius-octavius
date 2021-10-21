using System.Reflection;
using FluentValidation;
using GO.HostBuilder.Bootstrap.Pipelines;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace GO.HostBuilder.Bootstrap
{
    public static class MediatRBootstrapConfigurations
    {
        public static IServiceCollection RegisterGoService(
            this IServiceCollection services,
            Assembly assembly) =>
            services
                .AddMediatR(assembly)
                .RegisterFluentValidation(assembly);

        private static IServiceCollection RegisterFluentValidation(
            this IServiceCollection services,
            Assembly assembly)
        {
            AssemblyScanner
                .FindValidatorsInAssembly(assembly)
                .ForEach(a => services.AddScoped(a.InterfaceType, a.ValidatorType));

            return services.AddScoped(typeof(IPipelineBehavior<,>), typeof(MediatRPipelineValidationBehavior<,>));
        }
    }
}
