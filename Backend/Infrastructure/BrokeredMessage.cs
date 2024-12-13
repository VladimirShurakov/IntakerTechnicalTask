namespace Infrastructure;

public sealed class BrokeredMessage<TBody>: IBrokeredMessage<TBody>
{
    internal BrokeredMessage(TBody body)
    {
        Body = body;
    }

    public TBody Body { get; internal set; }
    public string? MessageId { get; internal init;}
    public string? PartitionKey { get; internal init;}
    public string? TransactionPartitionKey { get; internal init;}
    public string? SessionId { get; internal init;}
    public string? ReplyToSessionId { get; internal init;}
    public TimeSpan TimeToLive { get; internal init;}
    public string? CorrelationId { get; internal init;}
    public string? Subject { get; internal init;}
    public string? To { get; internal init;}
    public string? ContentType { get; internal init;}
    public string? ReplyTo { get; internal init;}
    public DateTimeOffset ScheduledEnqueueTime { get; internal init;}
}