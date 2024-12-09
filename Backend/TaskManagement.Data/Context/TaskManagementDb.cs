using Microsoft.EntityFrameworkCore;
using TaskStatus = TaskManagement.Data.Models.TaskStatus;
using Task = TaskManagement.Data.Models.Task;


namespace TaskManagement.Data.Context;

public class TaskManagementDb(DbContextOptions<TaskManagementDb> options) : DbContext(options)
{
    public DbSet<Task> Tasks { get; set; }
    public DbSet<TaskStatus> TaskStatuses { get; set; }
}