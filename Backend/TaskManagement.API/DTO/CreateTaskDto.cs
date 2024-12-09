namespace TaskManagement.API.DTO;
/// <summary>
/// View Task DTO
/// </summary>
public class CreateTaskDto
{
    /// <summary>
    /// The name of the task
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Task description
    /// </summary>
    public string Description { get; set; }
    
    /// <summary>
    /// Task assignee
    /// </summary>
    public string? AssignedTo { get; set; }
}