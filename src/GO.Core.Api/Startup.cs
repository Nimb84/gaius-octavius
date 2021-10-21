using GO.Core.Api.Bootstrap;
using GO.HostBuilder.Configurations;
using GO.Integration.TelegramBot;
using GO.Service.Movies;
using GO.Service.Users;

namespace GO.Core.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .Configure<ConfigurationRoot>(Configuration)
                .Configure<AppConfiguration>(Configuration.GetSection(nameof(AppConfiguration)))
                .AddOptions();

            services.RegisterControllers();
            services.RegisterSwagger();

            services.RegisterUserService(Configuration.GetConnectionString("GaiusBotUsersDb")!);
            services.RegisterMovieService(
                Configuration.GetConnectionString("GaiusBotMoviesDb")!,
                Configuration);

            services.AddLogging();

            services.RegisterTelegramBot(Configuration);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwaggerConfiguration();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
