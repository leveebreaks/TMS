using Azure.Messaging.ServiceBus;

namespace TMS.Infrastructure.Messaging;

public class AzureServiceBusHandler(string connectionString) : IMessageBrokerHandler
{
    private readonly ServiceBusClient _client = new ServiceBusClient(connectionString);

    public Task SendMessageAsync(string queue, object message)
    {
        throw new NotImplementedException();
    }

    public Task ReceiveMessageAsync(string queue, Func<object, Task> action)
    {
        throw new NotImplementedException();
    }
}