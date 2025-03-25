using TMS.DAL.Repositories;
using TMS.Domain.Models;
using TMS.Infrastructure.Messaging;
using TMS.Messages;
using TaskStatus = TMS.Domain.Models.TaskStatus;

namespace TMS.API.Services;

public class TaskService : ITaskService
{
    private readonly IRepository<TaskItem> _repository;
    private readonly IMessageBrokerHandler _messageBrokerHandler;

    public TaskService(IRepository<TaskItem> repository, IMessageBrokerHandler messageBrokerHandler)
    {
        this._repository = repository;
        this._messageBrokerHandler = messageBrokerHandler;
    }

    public async Task CreateTaskAsync(string name, string description, string assignedTo)
    {
        var message = new CreateNewTaskMessage
        {
            Name = name,
            Description = description,
            AssignedTo = assignedTo
        };
        await this._messageBrokerHandler.SendMessageAsync(message);
    }

    public async Task UpdateTaskStatusAsync(int taskId, string newStatus)
    {
        var message = new UpdateTaskMessage
        {
            Id = taskId,
            NewStatus = newStatus
        };
        await this._messageBrokerHandler.SendMessageAsync(message);
    }

    public async Task<List<TaskItem>> GetTasksAsync()
    {
        return await this._repository.GetAllAsync();
    }
}