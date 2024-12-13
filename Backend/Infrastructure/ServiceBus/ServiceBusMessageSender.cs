using Azure.Messaging.ServiceBus;
using Infrastructure.Extensions;

namespace Infrastructure.ServiceBus;

/// <summary>
/// Send a message to a Service Bus queue or topic
/// </summary>
/// <param name="client">Instance of <see cref="ServiceBusClient"/> class/></param>
public sealed class ServiceBusMessageSender(ServiceBusClient client) : IMessageSender
{
    private readonly ServiceBusClient _client = client ?? throw new ArgumentNullException(nameof(client));

    /// <summary>
    /// Send the message to the queue or topic
    /// </summary>
    /// <param name="queueOrTopic">The name of the queue or topic to send message to</param>
    /// <param name="message">Message to send</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <typeparam name="TBody">Instance of the payload to be sent</typeparam>
    public async ValueTask SendMessageAsync<TBody>(string queueOrTopic, IBrokeredMessage<TBody> message,
        CancellationToken cancellationToken = default)
    {
        await using var sender = _client.CreateSender(queueOrTopic);
        await sender.SendMessageAsync(message.ToServiceBusMessage(), cancellationToken);
    }
}