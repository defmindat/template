using EatWise.Common.Presentation.Endpoints;
using EatWise.Harvester.Application.Abstractions.Data;
using EatWise.Harvester.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EatWise.Harvester.Infrastructure;

public static class HarvesterModule
{
    public static IServiceCollection AddHarvesterModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddEndpoints(Presentation.AssemblyReference.Assembly);
        services.AddInfrastructure(configuration);
        return services;
    }

    private static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        string databaseConnectionString = configuration.GetConnectionString("Database")!;

        services.AddDbContext<HarvesterDbContext>((sp, options) =>
            options
                .UseNpgsql(
                    databaseConnectionString,
                    npgsqlOptions =>
                        npgsqlOptions.MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schemas.Harvesters))
                .UseSnakeCaseNamingConvention()
        );

        services.AddScoped<IUnitOfWork>(sp=> sp.GetRequiredService<HarvesterDbContext>());
    }
}
