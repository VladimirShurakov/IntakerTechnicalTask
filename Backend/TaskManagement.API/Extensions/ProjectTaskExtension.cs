using TaskManagement.API.DTO;
using TaskManagement.Data.Entities;

namespace TaskManagement.API.Extensions;

internal static class ProjectTaskExtension
{
    public static ProjectTask ToEntity(this CreateTaskDto taskDto, int statusId)
    {
        return new ProjectTask{
            Name = taskDto.Name,
            Description = taskDto.Description,
            StatusId = statusId,
            AssignedTo = taskDto.AssignedTo
        };
    }
}
