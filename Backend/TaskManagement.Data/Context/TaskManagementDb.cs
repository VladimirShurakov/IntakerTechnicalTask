using Microsoft.EntityFrameworkCore;
using TaskManagement.Data.Entities;

namespace TaskManagement.Data.Context;

public class TaskManagementDb(DbContextOptions<TaskManagementDb> options) : DbContext(options)
{
    public DbSet<ProjectTask> Tasks { get; set; }
    public DbSet<ProjectTaskStatus> TaskStatuses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
    }
}