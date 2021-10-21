using GO.Core.Api.Filters;
using GO.HostBuilder.Enums;
using GO.HostBuilder.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace GO.Core.Api.Bootstrap
{
    public static class ApiControllersConfigurations
    {
        public static void RegisterControllers(this IServiceCollection services)
        {
            services.AddControllers(options => options.Filters
                .Add(typeof(ExceptionResponseFilter)))
                .ConfigureApiBehaviorOptions(options =>
                {
                    options.InvalidModelStateResponseFactory = c =>
                    {
                        var errors = new List<ErrorDetails>();

                        foreach (var (key, state) in c.ModelState)
                        {
                            foreach (var error in state.Errors)
                            {
                                try
                                {
                                    errors.Add(
                                        Enum.TryParse<ExceptionType>(error.ErrorMessage, out var enumValue)
                                            ? new ErrorDetails(enumValue, enumValue.ToString(), key)
                                            : new ErrorDetails(ExceptionType.Validation, error.ErrorMessage, key));
                                }
                                catch
                                {
                                    // ignore
                                }
                            }
                        }

                        return new BadRequestObjectResult(new
                        {
                            Errors = errors
                        });
                    };
                })
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    options.SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.None;
                    options.SerializerSettings.Formatting = Formatting.Indented;
                });
        }
    }
}
