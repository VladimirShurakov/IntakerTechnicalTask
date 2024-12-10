namespace TaskManagement.API.DTO;
// ReSharper disable once ClassNeverInstantiated.Global

/// <summary>
/// Update task status DTO
/// </summary>
/// <param name="statusId">A new task status ID</param>
public class UpdateTaskDto(int statusId)
{
    /// <summary>
    /// A new task status ID
    /// </summary>
    public int StatusId { get; } = statusId;
}
