using Domain.Models;

namespace Application.Interfaces;

public interface IGenericRepository<T> where T:class
{
    Task<IEnumerable<Lesson>> GetAllAsync();
    Task<T> GetByIdAsync(int id);
    Task<Guid> AddAsync(T entity);
    Task<Guid> UpdateAsync(int id);
    Task<Guid> DeleteAsync(int id);
}