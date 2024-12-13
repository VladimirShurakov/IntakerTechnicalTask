using System.Text.Json;
using Azure.Messaging.ServiceBus;

namespace Infrastructure.Extensions;

/// <summary>
/// Extension methods for brokered message
/// </summary>
internal static class BrokeredMessageExtensions
{
    /// <summary>
    /// Create instance of <see cref="ServiceBusMessage"/> from the brokered message
    /// </summary>
    /// <param name="message">Instance brokered message to convert from</param>
    /// <typeparam name="TBody">Type of the message payload</typeparam>
    /// <returns>Instance of <see cref="ServiceBusMessage"/></returns>
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