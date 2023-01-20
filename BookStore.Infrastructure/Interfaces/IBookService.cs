using BookStore.Data.Entities;
using BookStore.Shared.Dto;

namespace BookStore.Infrastructure.Interfaces
{
    public interface IBookService
    {
        Task<BookDto?> GetAsync(int id, CancellationToken cancellationToken);
        Task<IEnumerable<BookDto>> GetAsync(int skip, int take, CancellationToken cancellationToken);
        Task<IEnumerable<BookDto>> GetAllAsync(CancellationToken cancellationToken);
        Task<BookDto?> FindByIdAsync(CancellationToken cancellationToken, int id);
        Task AddAsync(CreateBookDto createBookDto, CancellationToken cancellationToken);
        Task UpdateAsync(UpdateBookDto bookDto, CancellationToken cancellationToken);
        Task DeleteAsync(int id, CancellationToken cancellationToken);
    }
}
