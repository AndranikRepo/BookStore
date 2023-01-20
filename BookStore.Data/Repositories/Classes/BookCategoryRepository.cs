using BookStore.Data.Contexts;
using BookStore.Data.Entities;
using BookStore.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Data.Repositories.Classes
{
    public class BookCategoryRepository : IBookCategoryRepository
    {
        private readonly DatabaseContext _databaseContext;

        public BookCategoryRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<Dictionary<int, BookCategory>> GetByIdAsync(int bookId, CancellationToken cancellationToken)
        {
            return await _databaseContext.BookCategories.Where(x => x.BookId == bookId).ToDictionaryAsync(x => x.CategoryId, x => x);
        }
    }
}
