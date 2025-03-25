using TMS.Domain.Models;
using TaskStatus = TMS.Domain.Models.TaskStatus;

namespace TMS.API.Services;

public interface ITaskService
{
    Task CreateTaskAsync(string name, string description, string assignedTo);
    Task UpdateTaskStatusAsync(int taskId, string newStatus);
    Task<List<TaskItem>> GetTasksAsync();
}