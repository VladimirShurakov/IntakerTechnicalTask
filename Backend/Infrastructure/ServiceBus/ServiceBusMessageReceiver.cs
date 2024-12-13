using Azure.Messaging.ServiceBus;

namespace Infrastructure.ServiceBus;

/// <summary>
/// Receive messages from Service Bus queue
/// </summary>
public sealed class ServiceBusMessageReceiver : IMessageReceiver
{
    private readonly ServiceBusProcessor _processor;

    /// <summary>
    /// Creates an instance of a class
    /// </summary>
    /// <param name="queueName">The name of a queue</param>
    /// <param name="client">Instance of <see cref="ServiceBusClient"/> class. Managed by calling service.</param>
    /// <param name="messageHandler">Handle received messages</param>
    /// <param name="errorHandler">Handle any errors when receiving messages</param>
    /// <param name="processorOptions">Instance of <see cref="ServiceBusProcessorOptions"/> class.</param>
    public ServiceBusMessageReceiver(
        string queueName,
        ServiceBusClient client,
        Func<ProcessMessageEventArgs, Task> messageHandler,
        Func<ProcessErrorEventArgs, Task> errorHandler,
        ServiceBusProcessorOptions processorOptions)
    {
        if (string.IsNullOrWhiteSpace(queueName)) throw new ArgumentNullException(nameof(queueName));
        ArgumentNullException.ThrowIfNull(client);
        ArgumentNullException.ThrowIfNull(messageHandler);
        ArgumentNullException.ThrowIfNull(errorHandler);
        
        _processor = client.CreateProcessor(queueName, processorOptions);
        _processor.ProcessMessageAsync += messageHandler;
        _processor.ProcessErrorAsync += errorHandler;
    }

    /// <summary>
    /// Receive messages from the queue until it's empty and stop processing.
    /// </summary>
    /// <param name="cancellationToken"></param>
    public async ValueTask ReceiveAsync(CancellationToken cancellationToken = default)
    {
        await _processor.StartProcessingAsync(cancellationToken);
        await _processor.StopProcessingAsync(cancellationToken);
    }

    /// <summary>
    /// Dispose instance of <see cref="ServiceBusProcessor"/> class/>
    /// </summary>
    public ValueTask DisposeAsync()
    {
        return _processor.DisposeAsync();
    }
}