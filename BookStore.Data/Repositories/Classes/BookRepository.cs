using BookStore.Data.Contexts;
using BookStore.Data.Entities;
using BookStore.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BookStore.Data.Repositories.Classes
{
    public class BookRepository : IBookRepository
    {
        private readonly DatabaseContext _databaseContext;

        public BookRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<IEnumerable<Book>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _databaseContext.Books.Include(x => x.Author).Include(x => x.Categories).AsNoTracking().ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Book>> GetAsync(int skip, int take, CancellationToken cancellationToken)
        {
            return await _databaseContext.Books.Include(x => x.Author).Include(x => x.Categories).AsNoTracking().Skip(skip).Take(take).ToListAsync(cancellationToken);
        }

        public async Task<Book?> FindByIdAsync(CancellationToken cancellationToken, int id)
        {
            return await _databaseContext.Books.Include(x => x.Author).Include(x => x.Categories).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Book>> GetAllAsync(Expression<Func<Book, bool>> predicate, CancellationToken cancellationToken)
        {
            return await _databaseContext.Books.Include(x => x.Author).Include(x => x.Categories).ThenInclude(x => x.Category).Where(predicate).AsNoTracking().ToListAsync(cancellationToken);
        }
    }
}
