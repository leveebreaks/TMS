namespace TMS.Infrastructure.Messaging;

public interface IMessageBrokerHandler
{
    Task SendMessageAsync(string queue, object message);
    Task ReceiveMessageAsync(string queue, Func<object, Task> action);
}