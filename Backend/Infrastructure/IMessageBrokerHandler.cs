namespace Infrastructure;

public interface IMessageBrokerHandler : IAsyncDisposable
{
    ValueTask SendMessageAsync<TBody>(string queueOrTopic, IBrokeredMessage<TBody> message,
        CancellationToken cancellationToken = default);
}