﻿namespace TMS.Messages;

public class CreateNewTaskMessage : BaseMessage
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string AssignedTo { get; set; }
}