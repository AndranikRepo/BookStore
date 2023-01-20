using BookStore.Data.Entities;

namespace BookStore.Data.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAsync(int skip, int take, CancellationToken cancellationToken);
        Task<IEnumerable<int>> GetByIdsAsync(IEnumerable<int> entities, CancellationToken cancellationToken);
    }
}
