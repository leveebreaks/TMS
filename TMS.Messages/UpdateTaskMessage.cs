namespace TMS.Messages;

public class UpdateTaskMessage : BaseMessage
{
    public int Id { get; set; }
    public string NewStatus { get; set; }
}