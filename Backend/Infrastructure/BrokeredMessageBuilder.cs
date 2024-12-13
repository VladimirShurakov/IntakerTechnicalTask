namespace Infrastructure;

public sealed class BrokeredMessageBuilder<TBody>
{
    private TBody? _body;
    private string? _messageId;
    private string? _partitionKey;
    private string? _transactionPartitionKey;
    private string? _sessionId;
    private string? _replyToSessionId;
    private TimeSpan _timeToLive;
    private string? _correlationId;
    private string? _subject;
    private string? _to;
    private string? _replyTo;
    private DateTimeOffset _scheduledEnqueueTime;
    private string? _contentType;

    public BrokeredMessage<TBody> Build()
    {
        if(_body is null) throw new InvalidOperationException("Message body is null");
        var messageId = string.IsNullOrEmpty(_messageId) ? Guid.NewGuid().ToString() : _messageId;

        var message = new BrokeredMessage<TBody>(_body)
        {
            MessageId = messageId,
            PartitionKey = _partitionKey,
            TransactionPartitionKey = _transactionPartitionKey,
            SessionId = _sessionId,
            ReplyToSessionId = _replyToSessionId,
            TimeToLive = _timeToLive,
            CorrelationId = _correlationId,
            Subject = _subject,
            To = _to,
            ContentType = _contentType,
            ReplyTo = _replyTo,
            ScheduledEnqueueTime = _scheduledEnqueueTime
        };
        return message;
    }
    
    
    public BrokeredMessageBuilder<TBody> AddBody(TBody body)
    {
        _body = body ?? throw new ArgumentNullException(nameof(body));
        return this;
    }
    
    public BrokeredMessageBuilder<TBody> AddMessageId(string messageId)
    {
        _messageId = messageId ?? throw new ArgumentNullException(nameof(messageId));
        return this;
    }
  
    public BrokeredMessageBuilder<TBody> AddPartitionKey(string partitionKey)
    {
        _partitionKey = partitionKey ?? throw new ArgumentNullException(nameof(partitionKey));
        return this;
    }
    
    public BrokeredMessageBuilder<TBody> AddTransactionPartitionKey(string transactionPartitionKey)
    {
        _transactionPartitionKey = transactionPartitionKey ?? throw new ArgumentNullException(nameof(transactionPartitionKey));
        return this;
    }
    
    public BrokeredMessageBuilder<TBody> AddSessionId(string sessionId)
    {
        _sessionId = sessionId ?? throw new ArgumentNullException(nameof(sessionId));
        return this;
    }
 
    public BrokeredMessageBuilder<TBody> AddReplyToSessionId(string replyToSessionId)
    {
        _replyToSessionId = replyToSessionId ?? throw new ArgumentNullException(nameof(replyToSessionId));
        return this;
    }
    
    public BrokeredMessageBuilder<TBody> AddTimeToLive(TimeSpan timeToLive)
    {
        _timeToLive = timeToLive;
        return this;
    }
    
    public BrokeredMessageBuilder<TBody> AddCorrelationId(string correlationId)
    {
        _correlationId = correlationId ?? throw new ArgumentNullException(nameof(correlationId));
        return this;
    }
    
    public BrokeredMessageBuilder<TBody> AddSubject(string subject)
    {
        _subject = subject ?? throw new ArgumentNullException(nameof(subject));
        return this;
    }

    public BrokeredMessageBuilder<TBody> AddTo(string to)
    {
        _to = to ?? throw new ArgumentNullException(nameof(to));
        return this;
    }
    
    public BrokeredMessageBuilder<TBody> AddContentType(string contentType)
    {
        _contentType = contentType ?? throw new ArgumentNullException(nameof(contentType));
        return this;
    }
    
    public BrokeredMessageBuilder<TBody> AddReplyTo(string replyTo)
    {
        _replyTo = replyTo ?? throw new ArgumentNullException(nameof(replyTo));
        return this;
    }
    
    public BrokeredMessageBuilder<TBody> AddScheduledEnqueueTime(DateTimeOffset scheduledEnqueueTime)
    {
        _scheduledEnqueueTime = scheduledEnqueueTime;
        return this;
    }

    // public DateTimeOffset ScheduledEnqueueTime { get; internal init;}
}