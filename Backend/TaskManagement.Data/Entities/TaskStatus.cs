namespace TaskManagement.Data.Entities;

public class TaskStatus
{
    public int Id { get; set; }
    public string Name { get; set; }

    // Navigation properties
    public ICollection<Task> Tasks { get; } = new List<Task>();
}