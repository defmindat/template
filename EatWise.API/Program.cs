using EatWise.API.Extensions;
using EatWise.API.Middleware;
using EatWise.Common.Application;
using EatWise.Common.Infrastructure;
using EatWise.Common.Presentation.Endpoints;
using EatWise.Harvester.Infrastructure;
using EatWise.Users.Infrastructure;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Serilog;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, loggerConfig) => loggerConfig.ReadFrom.Configuration(context.Configuration));

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.CustomSchemaIds(t => t.FullName?.Replace("+", "."));
});

string databaseConnectionString = builder.Configuration.GetConnectionString("Database")!;

builder.Services.AddInfrastructure(databaseConnectionString);

builder.Configuration.AddModuleConfiguration(["harvesters", "users"]);

builder.Services.AddHealthChecks()
    .AddNpgSql(databaseConnectionString)
    .AddUrlGroup(new Uri(builder.Configuration.GetValue<string>("KeyCloak:HealthUrl")!), HttpMethod.Get, "keycloak");

builder.Services.AddHarvesterModule(builder.Configuration);
builder.Services.AddUsersModule(builder.Configuration);

builder.Services.AddApplication([EatWise.Harvester.Application.AssemblyReference.Assembly, EatWise.Users.Application.AssemblyReference.Assembly]);

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    
    app.ApplyMigrations();
}

app.MapEndpoints();

app.MapHealthChecks("health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.UseSerilogRequestLogging();

app.UseExceptionHandler();

app.UseAuthentication();
app.UseAuthorization();

app.Run();
