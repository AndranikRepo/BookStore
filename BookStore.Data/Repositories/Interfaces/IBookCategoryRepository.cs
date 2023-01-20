using BookStore.Data.Entities;

namespace BookStore.Data.Repositories.Interfaces
{
    public interface IBookCategoryRepository
    {
        Task<Dictionary<int, BookCategory>> GetByIdAsync(int id, CancellationToken cancellationToken);
    }
}
