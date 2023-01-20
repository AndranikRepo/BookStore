using BookStore.Data.Contexts;
using BookStore.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BookStore.Data.Repositories.Classes
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly DatabaseContext _databaseContext;
        private readonly DbSet<TEntity> _entity;

        public GenericRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
            _entity = _databaseContext.Set<TEntity>();
        }

        public async Task AddAsync(TEntity entity, CancellationToken cancellationToken)
        {
            await _entity.AddAsync(entity, cancellationToken);
        }

        public async Task AddAllAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken)
        {
            await _entity.AddRangeAsync(entities, cancellationToken);
        }

        public async Task<TEntity?> FindByIdAsync(CancellationToken cancellationToken, params object?[]? id)
        {
            return await _entity.FindAsync(id);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _entity.ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
        {
            return await _entity.Where(predicate).ToListAsync(cancellationToken);
        }

        public void Update(TEntity entity)
        {
            _entity.Entry(entity).State = EntityState.Modified;
        }

        public Task DeleteAsync(TEntity entity, CancellationToken cancellationToken)
        {
            _entity.Remove(entity);
            return Task.CompletedTask;
        }

        public void Attach(TEntity entity)
        {
            _entity.Attach(entity);
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return await _databaseContext.SaveChangesAsync(cancellationToken);
        }
    }
}
