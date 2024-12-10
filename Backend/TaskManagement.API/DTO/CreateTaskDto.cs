namespace TaskManagement.API.DTO;
// ReSharper disable once ClassNeverInstantiated.Global

/// <summary>
/// View Task DTO
/// </summary>
public class CreateTaskDto(string name, string description, string? assignedTo)
{
    /// <summary>
    /// The name of the task
    /// </summary>
    public string Name { get; } = name;

    /// <summary>
    /// Task description
    /// </summary>
    public string Description { get; } = description;

    /// <summary>
    /// Task assignee
    /// </summary>
    public string? AssignedTo { get; } = assignedTo;
}