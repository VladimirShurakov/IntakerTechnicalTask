using System.Text.Json;
using Azure.Messaging.ServiceBus;

namespace Infrastructure.Extensions;

public static class BrokeredMessageExtensions
{
    internal static ServiceBusMessage ToServiceBusMessage<TBody>(this IBrokeredMessage<TBody> message)
    {
        var serializedBody = JsonSerializer.Serialize(message.Body);
        var serviceBusMessage = new ServiceBusMessage(serializedBody)
        {
            MessageId = message.MessageId,
            PartitionKey = message.PartitionKey,
            TransactionPartitionKey = message.TransactionPartitionKey,
            SessionId = message.SessionId,
            ReplyToSessionId = message.ReplyToSessionId,
            TimeToLive = message.TimeToLive,
            CorrelationId = message.CorrelationId,
            Subject = message.Subject,
            To = message.To,
            ContentType = message.ContentType,
            ReplyTo = message.ReplyTo,
            ScheduledEnqueueTime = message.ScheduledEnqueueTime
        };
        return serviceBusMessage;
    }
}