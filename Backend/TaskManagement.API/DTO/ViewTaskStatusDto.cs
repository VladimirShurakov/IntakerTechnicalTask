using TaskStatus = TaskManagement.Data.Entities.TaskStatus;

namespace TaskManagement.API.DTO;

public class ViewTaskStatusDto(TaskStatus taskStatus)
{
    public int Id { get; } = taskStatus.Id;
    public string Name { get; } = taskStatus.Name;
}