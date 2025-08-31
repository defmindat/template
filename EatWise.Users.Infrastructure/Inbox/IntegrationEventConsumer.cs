using System.Data.Common;
using Dapper;
using EatWise.Common.Application.Data;
using EatWise.Common.Application.EventBus;
using EatWise.Common.Infrastructure.Inbox;
using EatWise.Common.Infrastructure.Serialization;
using MassTransit;
using Newtonsoft.Json;

namespace EatWise.Users.Infrastructure.Inbox;

internal sealed class IntegrationEventConsumer<TIntegrationEvent>(IDbConnectionFactory dbConnectionFactory)
    : IConsumer<TIntegrationEvent>
    where TIntegrationEvent : IntegrationEvent
{
    public async Task Consume(ConsumeContext<TIntegrationEvent> context)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();
        
        TIntegrationEvent integrationEvent = context.Message;

        var inbox = new InboxMessage
        {
            Id = integrationEvent.Id,
            Type = integrationEvent.GetType().Name,
            Content = JsonConvert.SerializeObject(integrationEvent, SerializerSettings.Instance),
            OccurredOnUtc = integrationEvent.OccurredOnUtc
        };
        
        const string sql = 
            """
            INSERT INTO users.inbox_message(id, type, content, occurred_on_utc)
            VALUES(@Id, @Type, @Content::json, @OccurredOnUtc)
            """;

        await connection.ExecuteAsync(sql, inbox);
    }
}
