using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using TMS.DAL.Data;
using TMS.DAL.Repositories;
using TMS.Domain.Models;
using TMS.Infrastructure.Messaging;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var sbConnectionString = builder.Configuration.GetConnectionString("ServiceBusConnectionString");
var sbTaskCommandQueue = builder.Configuration.GetConnectionString("ServiceBusTaskCommandsQueue");
var sbTaskEventQueue = builder.Configuration.GetConnectionString("ServiceBusTaskEventsQueue");
builder.Services.AddSingleton<IMessageBrokerHandler>(provider => 
    new AzureServiceBusHandler(sbConnectionString, sbTaskCommandQueue, sbTaskEventQueue));

var sqlConnectionString = builder.Configuration.GetConnectionString("SqlConnectionString");
builder.Services.AddDbContext<TaskDbContext>(options => options.UseSqlServer(sqlConnectionString));
builder.Services.AddScoped<IRepository<TaskItem>, TaskRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.MapGet("/tasks", async () =>
{
    return Results.Ok();
});

app.MapPost("/tasks", () =>
{

});

app.MapPatch("/tasks/{id}", () =>
{
    
});

app.Run();