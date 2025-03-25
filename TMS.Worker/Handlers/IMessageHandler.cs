using TMS.Messages;

namespace TMS.Worker.Handlers;

public interface IMessageHandler<T> where T : BaseMessage
{
    
}