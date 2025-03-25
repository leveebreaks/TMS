using System.Text.Json;
using Azure.Messaging.ServiceBus;
using TMS.Messages;

namespace TMS.Infrastructure.Messaging;

public class AzureServiceBusHandler : IMessageBrokerHandler, IAsyncDisposable
{
    private readonly ServiceBusClient _client;
    private readonly ServiceBusSender _sender;
    private readonly ServiceBusProcessor _processor;

    public AzureServiceBusHandler(string connectionString, string senderQueue, string receiverQueue)
    {
        this._client =  new ServiceBusClient(connectionString);
        this._sender = _client.CreateSender(senderQueue);
        this._processor = _client.CreateProcessor(receiverQueue);
    }

    public async Task SendMessageAsync<T>(T message) where T : BaseMessage
    {
        message.MessageType = typeof(T).Name;
        var messageJson = JsonSerializer.Serialize(message);
        var serviceBusMessage = new ServiceBusMessage(messageJson);
        await this._sender.SendMessageAsync(serviceBusMessage);
    }

    public async Task ProcessMessagesAsync(Func<object, Task> messageHandler, Func<object, Task> errorHandler)
    {
        this._processor.ProcessMessageAsync += async args =>
        {
            var er = args.Message;
            await messageHandler(args);
        };

        this._processor.ProcessErrorAsync += async args =>
        {
            await errorHandler(args);
            await Task.CompletedTask;
        };

        await this._processor.StartProcessingAsync();
    }

    public async ValueTask DisposeAsync()
    {
        await _sender.DisposeAsync();
        await _client.DisposeAsync();
        await _processor.DisposeAsync();
    }
}