using Microsoft.OpenApi.Models;

namespace EatWise.API.Extensions;

internal static class SwaggerExtensions
{
    internal static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "EatWise API",
                Version = "v1",
                Description = "EatWise API build using thw modalr monolith architecture.",
            });
            
            options.CustomSchemaIds(t => t.FullName?.Replace("+", "."));
        });
        
        return services;
    }
    
}
