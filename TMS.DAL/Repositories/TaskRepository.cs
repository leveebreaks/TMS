using TMS.DAL.Data;
using TMS.DAL.Models;

namespace TMS.DAL.Repositories;

public class TaskRepository(TaskDbContext context) : ITaskRepository
{
    public void AddTask(TaskItem item)
    {
        context.TaskItems.Add(item);
        context.SaveChanges();
    }

    public void UpdateTask(TaskItem item)
    {
        context.TaskItems.Update(item);
        context.SaveChanges();
    }

    public IEnumerable<TaskItem> GetAllTasks()
    {
        return context.TaskItems;
    }
}