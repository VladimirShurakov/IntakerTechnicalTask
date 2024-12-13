namespace Infrastructure;

/// <summary>
/// Message sender interface
/// </summary>
public interface IMessageSender
{
    /// <summary>
    /// Send message to the queue or topic
    /// </summary>
    /// <param name="queueOrTopic">Name of the queue or topic</param>
    /// <param name="message">Instance of the brokered message</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <typeparam name="TBody">Type of the message payload</typeparam>
    /// <returns></returns>
    ValueTask SendMessageAsync<TBody>(string queueOrTopic, IBrokeredMessage<TBody> message,
        CancellationToken cancellationToken = default);
}