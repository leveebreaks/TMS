using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using TMS.API.Services;
using TMS.DAL.Data;
using TMS.DAL.Repositories;
using TMS.Domain.Models;
using TMS.Infrastructure.Messaging;
using TMS.Messages;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var sbConnectionString = builder.Configuration.GetConnectionString("ServiceBusConnectionString");
var sbTaskCommandQueue = builder.Configuration["ServiceBusTaskCommandsQueue"];
var sbTaskEventQueue = builder.Configuration["ServiceBusTaskEventsQueue"];
Console.WriteLine(sbConnectionString,  sbTaskCommandQueue, sbTaskEventQueue);
builder.Services.AddSingleton<IMessageBrokerHandler>(provider => 
    new AzureServiceBusHandler(sbConnectionString, sbTaskCommandQueue, sbTaskEventQueue));

var sqlConnectionString = builder.Configuration.GetConnectionString("AzureSqlDbConnectionString");
builder.Services.AddDbContext<TaskDbContext>(options => options.UseSqlServer(sqlConnectionString));
builder.Services.AddScoped<IRepository<TaskItem>, TaskRepository>();

builder.Services.AddScoped<ITaskService, TaskService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.MapGet("/tasks", async (ITaskService taskService) =>
{
    var tasks = await taskService.GetTasksAsync();
    return Results.Ok(tasks);
});

app.MapPost("/tasks", async (ITaskService taskService) =>
{
    await taskService.CreateTaskAsync(
        "Configure Environment",
        "Set up the local environment, make sure all the services are configured and up",
        "John Johnson");
});

// app.MapPatch("/tasks/{id}", () =>
// {
//     
// });

app.Run();