namespace Interfaces
{
    public interface IRepository<T> where T : class, IEntity, new()
    {
        IQueryable<T> Items { get; }

        T Get(int id);
        Task<T> GetAsync(int id, CancellationToken cancellationToken = default);

        T Add(T item);
        Task<T> AddAsync(T item, CancellationToken cancellationToken = default);

        void Update(T item);
        Task UpdateAsync(T item, CancellationToken cancellationToken = default);

        void Delete(int id);
        Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    }
}
