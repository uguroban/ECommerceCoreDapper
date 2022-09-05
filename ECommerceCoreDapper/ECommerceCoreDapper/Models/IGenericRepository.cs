namespace ECommerceCoreDapper.Models
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        Task<T> GetByNameAsync(string name);
        Task<T> GetByDateAsync(DateTime date);
        Task<IReadOnlyList<T>> GetAllAsync();
        Task AddAsync(T entity);
        Task DeleteAsync(int id);
        Task UpdataAsync(T entity);
    }
}
