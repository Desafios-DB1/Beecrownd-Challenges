using BatalhaDePokemons.Domain.Models;

namespace BatalhaDePokemons.Domain.Repositories;

public interface IRepository<T> where T : class
{
    public Task AddAsync(T entity);
    public Task<T?> FindByIdAsync(Guid id);
    public Task<ICollection<T>> FindAllAsync();
    public Task UpdateAsync(T entity);
    public Task RemoveAsync(T entity);
}