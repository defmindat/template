using System.Data.Common;
using Dapper;
using EatWise.Common.Application.Data;
using EatWise.Common.Application.Messaging;
using EatWise.Common.Domain;
using EatWise.Common.Infrastructure.Outbox;

namespace EatWise.Users.Infrastructure.Outbox;

internal sealed class IdempotentDomainEventHandler<TDomainEvent>(
    IDomainEventHandler<TDomainEvent> decorated,
    IDbConnectionFactory dbConnectionFactory)
    : DomainEventHandler<TDomainEvent>
    where TDomainEvent : IDomainEvent
{
    public override async Task Handle(TDomainEvent domainEvent, CancellationToken cancellationToken = default)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();
        var outboxMessageConsumer = new OutboxMessageConsumer(domainEvent.Id, decorated.GetType().Name);

        if (await OutboxConsumerExistsAsync(connection, outboxMessageConsumer))
        {
            return;
        }
        
        await decorated.Handle(domainEvent, cancellationToken);
        
        await InsertOutboxConsumerAsync(connection, outboxMessageConsumer);
    }

    private static async Task<bool> OutboxConsumerExistsAsync(DbConnection connection,
        OutboxMessageConsumer outboxConsumer)
    {
        const string sql =
            """
            SELECT EXISTS(
                SELECT 1
                FROM users.outbox_message_consumers
                WHERE outbox_message_id = @OutboxMessageId AND
                      name = @Name)
            """;

        return await connection.ExecuteScalarAsync<bool>(sql, outboxConsumer);
    }

    private static async Task InsertOutboxConsumerAsync(
        DbConnection dbConnection,
        OutboxMessageConsumer outboxMessageConsumer)
    {
        const string sql = 
            """
            INSERT INTO users.outbox_message_consumers(outbox_message_id, name) 
            VALUES(@OutboxMessageId, @Name)
            """;
        
        await dbConnection.ExecuteAsync(sql, outboxMessageConsumer);
    }
}
