using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManagement.Data.Entities;

public class ProjectTaskStatus
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
    public required int Id { get; init; }
    
    [Required]
    [MaxLength(50)]
    public required string Name { get; init; }

    // Navigation properties
    [InverseProperty("ProjectTaskStatus")]
    public ICollection<ProjectTask> ProjectTasks { get; init; } = new List<ProjectTask>();
}