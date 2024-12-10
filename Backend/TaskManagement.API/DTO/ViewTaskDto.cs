using Task = TaskManagement.Data.Entities.Task;

namespace TaskManagement.API.DTO;
/// <summary>
/// View Task DTO
/// </summary>
public class ViewTaskDto(Task task)
{
    /// <summary>
    /// Task Identifier
    /// </summary>
    public int Id { get; } = task.Id;
    
    /// <summary>
    /// The name of the task
    /// </summary>
    public string Name { get; } = task.Name;
    
    /// <summary>
    /// Task description
    /// </summary>
    public string Description { get; } = task.Description;
    
    /// <summary>
    /// Current task status
    /// </summary>
    public string Status { get; } = task.Status.Name;
    
    /// <summary>
    /// Task assignee
    /// </summary>
    public string? AssignedTo { get; } = task.AssignedTo;
}
