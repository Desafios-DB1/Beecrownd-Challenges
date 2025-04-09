namespace BatalhaDePokemons.Domain.Repositories;

public interface IRepository<T> where T : class
{
    public Task SalvarAsync();
    public Task<Guid> AdicionarAsync(T entity);
    public Task<Guid> AdicionarESalvarAsync(T entity);
    public Task<T?> ObterPorIdAsync(Guid id);
    public Task<List<T>> ObterTodosAsync();
    public void Atualizar(T entity);
    public Task<T> AtualizarESalvarAsync(T entity);
    public void Remover(T entity);
    public Task RemoverESalvarAsync(T entity);
}