using TMS.Messages;

namespace TMS.Infrastructure.Messaging;

public interface IMessageBrokerHandler
{
    Task SendMessageAsync<T>(T message) where T : BaseMessage;
    Task ProcessMessagesAsync(Func<object, Task> messageHandler, Func<object, Task> errorHandler);
}