using DataAccessLayer.Context;
using DataAccessLayer.Entities.Base;
using Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer
{
    internal class DbRepository<T> : IRepository<T> where T : Entity, new()
    {
        private readonly StoreManagementDB _db;
        private readonly DbSet<T> _dbSet;

        public bool AutoSaveChanges { get; set; } = true;

        public DbRepository(StoreManagementDB db)
        {
            _db = db;
            _dbSet = db.Set<T>();
        }

        public IQueryable<T> Items => _dbSet;

        public T Get(int id) => Items.SingleOrDefault(item => item.Id == id);

        public async Task<T> GetAsync(int id, CancellationToken cancellationToken = default) => await Items
            .SingleOrDefaultAsync(item => item.Id == id)
            .ConfigureAwait(false);

        public T Add(T item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            _db.Entry(item).State = EntityState.Added;
            if (AutoSaveChanges)
                _db.SaveChanges();
            return item;
        }

        public async Task<T> AddAsync(T item, CancellationToken cancellationToken = default)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            _db.Entry(item).State = EntityState.Added;
            if (AutoSaveChanges)
                await _db.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            return item;
        }

        public void Delete(int id)
        {
            var item = _dbSet.Local.FirstOrDefault(i => i.Id == id) ?? new T { Id = id };

            _db.Remove(item);

            if (AutoSaveChanges)
                _db.SaveChanges();
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            _db.Remove(new T { Id = id });
            if (AutoSaveChanges)
                await _db.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }


        public void Update(T item)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));
            _db.Entry(item).State = EntityState.Modified;
            if (AutoSaveChanges)
                _db.SaveChanges();
        }

        public async Task UpdateAsync(T item, CancellationToken cancellationToken = default)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));
            _db.Entry(item).State = EntityState.Modified;
            if (AutoSaveChanges)
                await _db.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }
    }
}
