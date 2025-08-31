using Dapper;
using EatWise.Common.Application.Clock;
using EatWise.Common.Application.Data;
using EatWise.Common.Application.EventBus;
using EatWise.Common.Infrastructure.Authentication;
using EatWise.Common.Infrastructure.Authorization;
using EatWise.Common.Infrastructure.Clock;
using EatWise.Common.Infrastructure.Data;
using EatWise.Common.Infrastructure.Outbox;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Npgsql;
using Quartz;

namespace EatWise.Common.Infrastructure;

public static class InfrastructureConfiguration
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        Action<IRegistrationConfigurator>[] moduleConfigureConsumers,
        string databaseConnectionString)
    {
        services.AddAuthenticationInternal();

        services.AddAuthorizationInternal();
        
        services.TryAddSingleton<IDateTimeProvider, DateTimeProvider>();
        
        services.TryAddSingleton<IEventBus, EventBus.EventBus>();
        
        NpgsqlDataSource npgsqlDataSource = new NpgsqlDataSourceBuilder(databaseConnectionString).Build();
        services.TryAddSingleton(npgsqlDataSource);
        
        services.TryAddScoped<IDbConnectionFactory, DbConnectionFactory>();
        
        SqlMapper.AddTypeHandler(new GenericArrayHandler<string>());
        
        services.AddQuartz(configurator =>
        {
            var scheduler = Guid.NewGuid();
            configurator.SchedulerId = $"default-id-{scheduler}";
            configurator.SchedulerName = $"default-name-{scheduler}";
        });

        services.AddQuartzHostedService(options => options.WaitForJobsToComplete = true);

        
        services.TryAddSingleton<InsertOutboxMessagesInterceptor>();

        services.AddMassTransit(configure =>
        {
            foreach (Action<IRegistrationConfigurator> configureConsumers in moduleConfigureConsumers)
            {
                configureConsumers(configure);
            }
            
            configure.SetKebabCaseEndpointNameFormatter();

            configure.UsingInMemory((context, cfg) =>
            {
                cfg.ConfigureEndpoints(context);
            });
        });
        
        return services;
    }
}
