﻿using System.Data;
using System.Data.Common;
using Dapper;
using EatWise.Common.Application.Clock;
using EatWise.Common.Application.Data;
using EatWise.Common.Application.Messaging;
using EatWise.Common.Domain;
using EatWise.Common.Infrastructure.Outbox;
using EatWise.Common.Infrastructure.Serialization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Quartz;

namespace EatWise.Users.Infrastructure.Outbox;

[DisallowConcurrentExecution]
internal sealed class ProcessOutboxJob(
    IDbConnectionFactory dbConnectionFactory,
    IServiceScopeFactory serviceScopeFactory,
    IDateTimeProvider dateTimeProvider,
    IOptions<OutboxOptions> outboxOptions,
    ILogger<ProcessOutboxJob> logger) : IJob
{
    private const string ModuleName = "Users";

    public async Task Execute(IJobExecutionContext context)
    {
        logger.LogInformation("{Module} - Beginning to process outbox messages", ModuleName);

        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();
        await using DbTransaction transaction = await connection.BeginTransactionAsync();
        
        IReadOnlyList<OutboxMessageResponse> outboxMessages = await GetOutboxMessagesAsync(connection, transaction);

        foreach (OutboxMessageResponse outboxMessage in outboxMessages)
        {
            Exception? exception = null;
            try
            {
                IDomainEvent domainEvent =
                    JsonConvert.DeserializeObject<IDomainEvent>(outboxMessage.Content, SerializerSettings.Instance)!;

                using IServiceScope scope = serviceScopeFactory.CreateScope();

                IEnumerable<IDomainEventHandler> domainEventHandlers = DomainEventHandlersFactory.GetHandlers(
                    domainEvent.GetType(),
                    scope.ServiceProvider,
                    Application.AssemblyReference.Assembly);

                foreach (IDomainEventHandler domainEventHandler in domainEventHandlers)
                {
                    await domainEventHandler.Handle(domainEvent);
                }
            }
            catch (Exception caughtException)
            {
                logger.LogError(
                    caughtException,
                    "{Module} - Exception while processing outbox message {MessageId}",
                    ModuleName,
                    outboxMessage.Id);
            }
            
            await UpdateOutboxMessageAsync(connection, transaction, outboxMessage, exception);
        }
        
        await transaction.CommitAsync();
        
        logger.LogInformation("{Module} - Completed processing outbox messages", ModuleName);
    }

    private async Task<IReadOnlyList<OutboxMessageResponse>> GetOutboxMessagesAsync(
        IDbConnection connection,
        IDbTransaction transaction)
    {
        string sql =
            $"""
             SELECT 
                id AS {nameof(OutboxMessageResponse.Id)},
                content AS {nameof(OutboxMessageResponse.Content)}
             FROM users.outbox_messages
             WHERE processed_on_utc IS NULL
             ORDER BY occurred_on_utc
             LIMIT {outboxOptions.Value.BatchSize}
             FOR UPDATE
             """;

        IEnumerable<OutboxMessageResponse> outboxMessages = await connection.QueryAsync<OutboxMessageResponse>(
            sql,
            transaction: transaction);

        return outboxMessages.ToList();
    }

    private async Task UpdateOutboxMessageAsync(
        IDbConnection dbConnection,
        IDbTransaction transaction,
        OutboxMessageResponse outboxMessage,
        Exception? exception)
    {
        const string sql = 
            """
            UPDATE users.outbox_messages
            SET processed_on_utc = @ProcessOnUtc,
                error = @Error
            WHERE id = @Id
            """;
        
        await dbConnection.ExecuteAsync(
            sql,
            new
            {
                outboxMessage.Id,
                ProcessedOnUtc = dateTimeProvider.UtcNow,
                Error = exception?.ToString()
            },
            transaction: transaction);
    }
    internal sealed record OutboxMessageResponse(Guid Id, string Content);
}
