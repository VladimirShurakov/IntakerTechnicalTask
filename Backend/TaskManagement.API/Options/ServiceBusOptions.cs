namespace TaskManagement.API.Options;

internal sealed class ServiceBusOptions
{
    public required string ConnectionOrFqdn { get; init; }
    public required string QueueOrTopicName { get; init; }
    public int RetryCount { get; init; }
    public int RetryDelayMs { get; init; }
    public int MessageTimeToLiveDays { get; init; }
}