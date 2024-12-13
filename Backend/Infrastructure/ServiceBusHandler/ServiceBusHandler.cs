using Azure.Messaging.ServiceBus;
using Infrastructure.Extensions;

namespace Infrastructure.ServiceBusHandler;

public class ServiceBusHandler(ServiceBusClient client) : IMessageBrokerHandler
{
    private readonly ServiceBusClient _client = client ?? throw new ArgumentNullException(nameof(client));
    
    public async ValueTask SendMessageAsync<TBody>(string queueOrTopic, IBrokeredMessage<TBody> message,
        CancellationToken cancellationToken = default)
    {
        await using var sender = client.CreateSender(queueOrTopic);
        await sender.SendMessageAsync(message.ToServiceBusMessage(), cancellationToken);
    }

    public async ValueTask DisposeAsync()
    {
        await _client.DisposeAsync();
    }
}