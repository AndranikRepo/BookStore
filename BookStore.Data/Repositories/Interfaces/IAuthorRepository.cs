using BookStore.Data.Entities;

namespace BookStore.Data.Repositories.Interfaces
{
    public interface IAuthorRepository
    {
        Task<IEnumerable<Author>> GetAsync(int skip, int take, CancellationToken cancellationToken);
    }
}
