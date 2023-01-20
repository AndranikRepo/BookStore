using System.Linq.Expressions;

namespace BookStore.Data.Repositories.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken);
        Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);
        Task<TEntity?> FindByIdAsync(CancellationToken cancellationToken, params object?[]? id);
        Task AddAsync(TEntity entity, CancellationToken cancellationToken);
        Task AddAllAsync(IEnumerable<TEntity>? entities, CancellationToken cancellationToken = default);
        void Update(TEntity entity);
        Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
