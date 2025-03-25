using TMS.Domain.Models;

namespace TMS.DAL.Repositories;

public interface IRepository<T> where T : class
{
    Task AddAsync(T item);
    Task UpdateAsync(T item);
    Task<IEnumerable<T>> GetAllAsync();
}