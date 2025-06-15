﻿using EatWise.Common.Application.Data;
using EatWise.Common.Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Npgsql;

namespace EatWise.Common.Infrastructure;

public static class InfrastructureConfiguration
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services, 
        string databaseConnectionString)
    {
        NpgsqlDataSource npgsqlDataSource = new NpgsqlDataSourceBuilder(databaseConnectionString).Build();
        services.TryAddSingleton(npgsqlDataSource);
        
        services.TryAddScoped<IDbConnectionFactory, DbConnectionFactory>();
        
        return services;
    }
}