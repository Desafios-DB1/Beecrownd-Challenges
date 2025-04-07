namespace BatalhaDePokemons.Domain.Repositories;

public interface IRepository<T> where T : class
{
    public Task SaveChangesAsync();
    public Task<Guid> AddAsync(T entity);
    public Task<Guid> AddAndCommitAsync(T entity);
    public Task<T?> FindByIdAsync(Guid id);
    public Task<List<T>> FindAllAsync();
    public void Update(T entity);
    public Task<T> UpdateAndCommitAsync(T entity);
    public void Remove(T entity);
    public Task RemoveAndCommitAsync(T entity);
}