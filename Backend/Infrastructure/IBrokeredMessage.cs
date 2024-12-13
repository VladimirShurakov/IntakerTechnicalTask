namespace Infrastructure;

public interface IBrokeredMessage<out TMessage>
{
    TMessage Body { get; }
    string? MessageId { get; }
    string? PartitionKey { get; }
    string? TransactionPartitionKey { get; }
    string? SessionId { get; }
    string? ReplyToSessionId { get; }
    TimeSpan TimeToLive { get; }
    string? CorrelationId { get; }
    string? Subject { get; }
    string? To { get; }
    string? ContentType { get; }
    string? ReplyTo { get; }
    DateTimeOffset ScheduledEnqueueTime { get; }
}