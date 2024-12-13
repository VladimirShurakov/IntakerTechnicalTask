namespace Infrastructure;

/// <summary>
/// Message receiver interface
/// </summary>
public interface IMessageReceiver : IAsyncDisposable
{
    /// <summary>
    /// Receives the messages
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>A running instance of <see cref="ValueTask{TResult}"/></returns>
    ValueTask ReceiveAsync(CancellationToken cancellationToken = default);
}