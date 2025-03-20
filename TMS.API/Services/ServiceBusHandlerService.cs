namespace TMS.API.Services;

public interface IMessageBrokerService
{
    Task SendMessage(string message);
    Task ReceiveMessage(string message);
}

public class ServiceBusHandlerService : IMessageBrokerService
{
    public Task SendMessage(string message)
    {
        throw new NotImplementedException();
    }

    public Task ReceiveMessage(string message)
    {
        throw new NotImplementedException();
    }
}