using BookStore.Shared.Dto;

namespace BookStore.Infrastructure.Interfaces
{
    public interface ICategoryService
    {
        Task<CategoryDto?> GetAsync(int id, CancellationToken cancellationToken);
        Task<IEnumerable<CategoryDto>> GetAsync(int skip, int take, CancellationToken cancellationToken);
        Task<IEnumerable<CategoryDto>> GetAllAsync(CancellationToken cancellationToken);
        Task<CategoryDto?> FindByIdAsync(CancellationToken cancellationToken, params object?[]? id);
        Task AddAsync(CreateCategoryDto createCategoryDto, CancellationToken cancellationToken);
        Task UpdateAsync(CategoryDto categoryDto, CancellationToken cancellationToken);
        Task DeleteAsync(int id, CancellationToken cancellationToken);
    }
}
