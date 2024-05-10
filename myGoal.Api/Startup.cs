using Asp.Versioning;
using Microsoft.OpenApi.Models;

namespace myGoal.Api;

public static class Startup
{
    /// <summary>
    /// Підключення Swagger
    /// </summary>
    /// <param name="services"></param>
    public static void AddSwagger(this IServiceCollection services)
    {
        services.AddApiVersioning()
            .AddApiExplorer(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
            });
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1",new OpenApiInfo()
            {
                Version = "v1",
                Title = "myGoal.Api",
                Description = "This is version 1.0",
                Contact = new OpenApiContact()
                {
                    Name = "Roman K."
                },
            });

        });
    }
    
}