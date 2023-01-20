using BookStore.Shared.Dto;

namespace BookStore.Infrastructure.Interfaces
{
    public interface IAuthorService
    {
        Task<AuthorDto?> GetAsync(int id, CancellationToken cancellationToken);
        Task<IEnumerable<AuthorDto>> GetAsync(int skip, int take, CancellationToken cancellationToken);
        Task<IEnumerable<AuthorDto>> GetAllAsync(CancellationToken cancellationToken);
        Task<AuthorDto?> FindByIdAsync(CancellationToken cancellationToken, params object?[]? id);
        Task AddAsync(CreateAuthorDto createAuthorDto, CancellationToken cancellationToken);
        Task UpdateAsync(AuthorDto authorDto, CancellationToken cancellationToken);
        Task DeleteAsync(int id, CancellationToken cancellationToken);
    }
}
