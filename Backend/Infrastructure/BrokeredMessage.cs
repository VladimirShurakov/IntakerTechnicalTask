namespace Infrastructure;

/// <summary>
/// Default implementation of <see cref="IBrokeredMessage{TMessage}"/> interface
/// </summary>
/// <typeparam name="TBody">Type of the brokered message payload</typeparam>
public sealed class BrokeredMessage<TBody> : IBrokeredMessage<TBody>
{
    /// <summary>
    /// Internal Ctor
    /// </summary>
    /// <param name="body">Instance of payload</param>
    internal BrokeredMessage(TBody body)
    {
        Body = body;
    }

    public TBody Body { get; }
    public string? MessageId { get; internal init; }
    public string? PartitionKey { get; internal init; }
    public string? TransactionPartitionKey { get; internal init; }
    public string? SessionId { get; internal init; }
    public string? ReplyToSessionId { get; internal init; }
    public TimeSpan TimeToLive { get; internal init; }
    public string? CorrelationId { get; internal init; }
    public string? Subject { get; internal init; }
    public string? To { get; internal init; }
    public string? ContentType { get; internal init; }
    public string? ReplyTo { get; internal init; }
    public DateTimeOffset ScheduledEnqueueTime { get; internal init; }
}