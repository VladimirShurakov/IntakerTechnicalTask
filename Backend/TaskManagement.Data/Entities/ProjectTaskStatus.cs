using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManagement.Data.Entities;

public class ProjectTaskStatus
{
    [Key]
    public required int Id { get; set; }
    
    [Required]
    [MaxLength(50)]
    public required string Name { get; set; }

    // Navigation properties
    [InverseProperty("ProjectTaskStatus")]
    public required ICollection<ProjectTask> ProjectTasks { get; init; } = new List<ProjectTask>();
}