﻿using Microsoft.OpenApi.Models;

namespace GO.Core.Api.Bootstrap
{
    public static class SwaggerConfigurations
    {
        public static void RegisterSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(setup =>
            {
                setup.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "GaiusOctavius API",
                    Version = "v1"
                });
            });
        }

        public static void UseSwaggerConfiguration(this IApplicationBuilder app)
        {
            app.UseSwagger(c => c.RouteTemplate = "_doc/api/{documentName}/swagger.json");

            app.UseSwaggerUI(c =>
            {
                c.DocumentTitle = "Gaius Octavius API";
                c.RoutePrefix = "_doc/api";
                c.SwaggerEndpoint("./v1/swagger.json", "V1 Docs");
                c.OAuthRealm("Swagger");
                c.OAuthAppName("Swagger");
            });
        }
    }
}
