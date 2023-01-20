using BookStore.Data.Contexts;
using BookStore.Data.Entities;
using BookStore.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Data.Repositories.Classes
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly DatabaseContext _databaseContext;

        public AuthorRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<IEnumerable<Author>> GetAsync(int skip, int take, CancellationToken cancellationToken)
        {
            return await _databaseContext.Authors.AsNoTracking().Skip(skip).Take(take).ToListAsync(cancellationToken);
        }
    }
}
