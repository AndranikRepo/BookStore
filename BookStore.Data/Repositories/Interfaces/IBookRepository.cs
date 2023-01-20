using BookStore.Data.Entities;
using System.Linq.Expressions;

namespace BookStore.Data.Repositories.Interfaces
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetAllAsync(CancellationToken cancellationToken);
        Task<IEnumerable<Book>> GetAsync(int skip, int take, CancellationToken cancellationToken);
        Task<IEnumerable<Book>> GetAllAsync(Expression<Func<Book, bool>> predicate, CancellationToken cancellationToken);
        Task<Book?> FindByIdAsync(CancellationToken cancellationToken, int id);
    }
}
