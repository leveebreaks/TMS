using Microsoft.EntityFrameworkCore;
using TMS.DAL.Data;
using TMS.Domain.Models;

namespace TMS.DAL.Repositories;

public class TaskRepository : IRepository<TaskItem>
{
    private readonly TaskDbContext _context;

    public TaskRepository(TaskDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(TaskItem item)
    {
        _context.TaskItems.Add(item);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(TaskItem item)
    {
        _context.TaskItems.Update(item);
        await _context.SaveChangesAsync();
    }

    public async Task<List<TaskItem>> GetAllAsync()
    {
        return await _context.TaskItems.ToListAsync();
    }
}