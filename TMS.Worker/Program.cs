using TMS.DAL.Repositories;
using TMS.Worker;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();
builder.Services.AddScoped<ITaskRepository<TaskItem>, TaskRepository>();

var host = builder.Build();

host.Run();