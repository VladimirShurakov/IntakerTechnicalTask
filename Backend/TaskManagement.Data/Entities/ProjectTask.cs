﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManagement.Data.Entities;

public class ProjectTask
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; init; }
    
    [Required, MaxLength(50)]
    public required string Name { get; init; }
    
    [Required, MaxLength(2000)]
    public required string Description { get; init; }
    
    [MaxLength(120)]
    public string? AssignedTo { get; init; }
    
    [Required, ForeignKey(nameof(ProjectTaskStatus))]
    public required int StatusId { get; set; }
    
    // Navigation properties
    public required ProjectTaskStatus ProjectTaskStatus { get; init; }
}