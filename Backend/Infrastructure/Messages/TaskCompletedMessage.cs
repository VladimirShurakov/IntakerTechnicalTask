namespace Infrastructure.Messages;

public sealed class TaskCompletedMessage
{
    public int TaskId { get; init; }
    public DateTime CompletionDate { get; init; }
}