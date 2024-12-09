using TaskManagement.Data.Models;
using Task = TaskManagement.Data.Models.Task;

namespace TaskManagement.API.DTO;
/// <summary>
/// View Task DTO
/// </summary>
public class ViewTaskDto
{
    /// <summary>
    /// Default CTOR
    /// </summary>
    public ViewTaskDto() { }

    /// <summary>
    /// To create a View DTO from DB model
    /// </summary>
    /// <param name="task">DB instance of Task</param>
    public ViewTaskDto(Task task) => (Id, Name, Description, Status, AssignedTo) =
        (task.Id, task.Name, task.Description, task.Status.Name, task.AssignedTo);

    /// <summary>
    /// Task Identifier
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// The name of the task
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Task description
    /// </summary>
    public string Description { get; set; }
    
    /// <summary>
    /// Current task status
    /// </summary>
    public string Status { get; set; }
    
    /// <summary>
    /// Task assignee
    /// </summary>
    public string? AssignedTo { get; set; }
}