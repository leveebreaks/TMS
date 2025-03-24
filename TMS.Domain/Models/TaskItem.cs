namespace TMS.Domain.Models;

public enum TaskStatus
{
    NotStarted,
    InProgress,
    Completed
}

public class TaskItem
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public TaskStatus Status { get; set; }
    public string? AssignedTo { get; set; }
}