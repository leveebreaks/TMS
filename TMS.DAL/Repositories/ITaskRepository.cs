using TMS.DAL.Models;

namespace TMS.DAL.Repositories;

public interface ITaskRepository
{
    void AddTask(TaskItem item);
    void UpdateTask(TaskItem item);
    IEnumerable<TaskItem> GetAllTasks();
}