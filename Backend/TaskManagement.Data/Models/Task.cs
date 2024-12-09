﻿namespace TaskManagement.Data.Models;

public class Task
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public TaskStatus Status { get; set; }
    public string? AssignedTo { get; set; } 
}