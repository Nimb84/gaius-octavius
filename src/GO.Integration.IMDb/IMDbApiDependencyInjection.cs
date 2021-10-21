using GO.Integration.IMDb.Abstractions;
using GO.Integration.IMDb.Configurations;
using GO.Integration.IMDb.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GO.Integration.IMDb
{
    public static class IMDbApiDependencyInjection
    {
        public static IServiceCollection RegisterIMDbApi(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.Configure<IMDbConfiguration>(configuration.GetRequiredSection(nameof(IMDbConfiguration)));

            services.AddTransient<IMovieService, IMDbApiService>();

            return services;
        }
    }
}
