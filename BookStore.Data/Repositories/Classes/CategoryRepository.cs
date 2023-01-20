using BookStore.Data.Contexts;
using BookStore.Data.Entities;
using BookStore.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Data.Repositories.Classes
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DatabaseContext _databaseContext;

        public CategoryRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<IEnumerable<int>> GetByIdsAsync(IEnumerable<int> entities, CancellationToken cancellationToken)
        {
            return await _databaseContext.Categories.Where(x => entities.Contains(x.Id))
                .Select(x => x.Id)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Category>> GetAsync(int skip, int take, CancellationToken cancellationToken)
        {
            return await _databaseContext.Categories.AsNoTracking().Skip(skip).Take(take).ToListAsync(cancellationToken);
        }
    }
}
