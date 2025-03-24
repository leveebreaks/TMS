using Microsoft.EntityFrameworkCore;
using TMS.DAL.Models;

namespace TMS.DAL.Data;

public class TaskDbContext(DbContextOptions<TaskDbContext> options) : DbContext(options)
{
    public DbSet<TaskItem> TaskItems { get; set; }
}