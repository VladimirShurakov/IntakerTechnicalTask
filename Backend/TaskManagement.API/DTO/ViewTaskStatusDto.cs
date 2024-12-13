using TaskManagement.Data.Entities;

namespace TaskManagement.API.DTO;

public class ViewTaskStatusDto(ProjectTaskStatus taskStatus)
{
    public int Id { get; } = taskStatus.Id;
    public string Name { get; } = taskStatus.Name;
}