using AutoMapper;
using BookStore.Data.Entities;
using BookStore.Data.Repositories.Interfaces;
using BookStore.Shared.Dto;
using BookStore.Shared;
using BookStore.Infrastructure.Interfaces;

namespace BookStore.Infrastructure.Classes
{
    public class CategoryService : ICategoryService
    {
        private readonly IGenericRepository<Category> _genericRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(IGenericRepository<Category> genericRepository,
            ICategoryRepository categoryRepository,
            IMapper mapper)
        {
            _genericRepository = genericRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<CategoryDto?> GetAsync(int id, CancellationToken cancellationToken)
        {
            var category = await _genericRepository.FindByIdAsync(cancellationToken, id);

            return category != null
                ? _mapper.Map<Category, CategoryDto>(category)
                : null;
        }

        public async Task<IEnumerable<CategoryDto>> GetAsync(int skip, int take, CancellationToken cancellationToken)
        {
            return _mapper.Map<IEnumerable<Category>, IEnumerable<CategoryDto>>(await _categoryRepository.GetAsync(skip, take, cancellationToken));
        }

        public async Task<IEnumerable<CategoryDto>> GetAllAsync(CancellationToken cancellationToken)
        {
            return _mapper.Map<IEnumerable<Category>, IEnumerable<CategoryDto>>(await _genericRepository.GetAllAsync(cancellationToken));
        }

        public async Task<IEnumerable<CategoryDto>> GetAllAsync(int skip, int take, CancellationToken cancellationToken)
        {
            return _mapper.Map<IEnumerable<Category>, IEnumerable<CategoryDto>>(await _categoryRepository.GetAsync(skip, take, cancellationToken));
        }

        public async Task<CategoryDto?> FindByIdAsync(CancellationToken cancellationToken, params object?[]? id)
        {
            var category = await _genericRepository.FindByIdAsync(cancellationToken, id);

            return category!= null
                ? _mapper.Map<Category, CategoryDto>(category)
                : null;
        }

        public async Task AddAsync(CreateCategoryDto createCategoryDto, CancellationToken cancellationToken)
        {
            await _genericRepository.AddAsync(_mapper.Map<CreateCategoryDto, Category>(createCategoryDto), cancellationToken);
            await _genericRepository.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(CategoryDto categoryDto, CancellationToken cancellationToken)
        {
            var category = _mapper.Map<CategoryDto, Category>(categoryDto);
            _genericRepository.Update(category);
            await _genericRepository.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var category = await _genericRepository.FindByIdAsync(cancellationToken, id);

            if (category == null) throw new ArgumentException(ExceptionMessages.EntityNotFound);

            await _genericRepository.DeleteAsync(category, cancellationToken);
            await _genericRepository.SaveChangesAsync(cancellationToken);
        }
    }
}
