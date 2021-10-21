using System.Reflection;
using GO.HostBuilder.Bootstrap;
using GO.Integration.IMDb;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GO.Service.Movies
{
    public static class MoviesServiceDependencyInjection
    {
        public static IServiceCollection RegisterMovieService(
            this IServiceCollection services,
            string connectionString,
            IConfiguration configuration) =>
            services.RegisterGoService(Assembly.GetExecutingAssembly())
                .AddDbContext<MovieDbContext>(options => options.UseSqlServer(connectionString))
                .RegisterIMDbApi(configuration);
    }
}
