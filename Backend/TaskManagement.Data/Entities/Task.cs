namespace TaskManagement.Data.Entities;

public class Task
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string? AssignedTo { get; set; }
    public int StatusId { get; set; }
    
    // Navigation properties
    public TaskStatus Status { get; set; }
}
