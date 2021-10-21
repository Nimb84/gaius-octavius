using System.Reflection;
using GO.HostBuilder.Bootstrap;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GO.Service.Users
{
    public static class UserServiceDependencyInjection
    {
        public static IServiceCollection RegisterUserService(
            this IServiceCollection services,
            string connectionString) =>
            services
                .RegisterGoService(Assembly.GetExecutingAssembly())
                .AddDbContext<UserDbContext>(options => options.UseSqlServer(connectionString));
    }
}
