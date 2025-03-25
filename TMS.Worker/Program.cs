using TMS.DAL.Repositories;
using TMS.Domain.Models;
using TMS.Messages;
using TMS.Worker;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();
builder.Services.AddScoped<IRepository<TaskItem>, TaskRepository>();

var host = builder.Build();

host.Run();