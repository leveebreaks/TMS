using Microsoft.EntityFrameworkCore;
using TMS.DAL.Data;
using TMS.DAL.Repositories;
using TMS.Domain.Models;
using TMS.Infrastructure.Messaging;
using TMS.Messages;
using TMS.Worker;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();

var sbConnectionString = builder.Configuration.GetConnectionString("ServiceBusConnectionString");
var sbTaskCommandQueue = builder.Configuration.GetConnectionString("ServiceBusTaskCommandsQueue");
var sbTaskEventQueue = builder.Configuration.GetConnectionString("ServiceBusTaskEventsQueue");
builder.Services.AddSingleton<IMessageBrokerHandler>(provider => 
    new AzureServiceBusHandler(sbConnectionString, sbTaskEventQueue, sbTaskCommandQueue));

var sqlConnectionString = builder.Configuration.GetConnectionString("AzureSqlDbConnectionString");
builder.Services.AddDbContext<TaskDbContext>(options => options.UseSqlServer(sqlConnectionString));
builder.Services.AddScoped<IRepository<TaskItem>, TaskRepository>();

var host = builder.Build();

host.Run();