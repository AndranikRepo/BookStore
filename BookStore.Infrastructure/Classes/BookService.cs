using AutoMapper;
using BookStore.Data.Entities;
using BookStore.Data.Repositories.Interfaces;
using BookStore.Infrastructure.Interfaces;
using BookStore.Shared;
using BookStore.Shared.Dto;

namespace BookStore.Infrastructure.Classes
{
    public class BookService : IBookService
    {
        private readonly IGenericRepository<Book> _genericRepositoryBook;
        private readonly IGenericRepository<BookCategory> _genericRepositoryBookCategory;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IBookCategoryRepository _bookCategoryRepository;
        private readonly IMapper _mapper;

        public BookService(IGenericRepository<Book> genericRepositoryBook,
            IGenericRepository<Category> genericRepositoryCategory,
            IGenericRepository<BookCategory> genericRepositoryBookCategory,
            ICategoryRepository categoryRepository,
            IBookRepository bookRepository,
            IBookCategoryRepository bookCategoryRepository,
            IMapper mapper)
        {
            _genericRepositoryBook = genericRepositoryBook;
            _genericRepositoryBookCategory = genericRepositoryBookCategory;
            _categoryRepository = categoryRepository;
            _bookRepository = bookRepository;
            _bookCategoryRepository = bookCategoryRepository;
            _mapper = mapper;
        }

        public async Task<BookDto?> GetAsync(int id, CancellationToken cancellationToken)
        {
            var book = await _genericRepositoryBook.FindByIdAsync(cancellationToken, id);

            return book != null
                ? _mapper.Map<Book, BookDto>(book)
                : null;
        }

        public async Task<IEnumerable<BookDto>> GetAsync(int skip, int take, CancellationToken cancellationToken)
        {
            return _mapper.Map<IEnumerable<Book>, IEnumerable<BookDto>>(await _bookRepository.GetAsync(skip, take, cancellationToken));
        }

        public async Task<IEnumerable<BookDto>> GetAllAsync(CancellationToken cancellationToken)
        {
            return _mapper.Map<IEnumerable<Book>, IEnumerable<BookDto>>(await _bookRepository.GetAllAsync(x => !x.IsRemoved, cancellationToken));
        }

        public async Task<BookDto?> FindByIdAsync(CancellationToken cancellationToken, int id)
        {
            var book = await _bookRepository.FindByIdAsync(cancellationToken, id);

            return book != null
                ? _mapper.Map<Book, BookDto>(book)
                : null;
        }

        public async Task AddAsync(CreateBookDto createBookDto, CancellationToken cancellationToken)
        {
            var book = _mapper.Map<CreateBookDto, Book>(createBookDto);

            var categoryIds = await _categoryRepository.GetByIdsAsync(createBookDto.CategoryIds, cancellationToken);

            if (categoryIds.Count() == 0) throw new ArgumentException(ExceptionMessages.CategoryNotFound);

            await _genericRepositoryBook.AddAsync(book, cancellationToken);
            await _genericRepositoryBook.SaveChangesAsync();

            var bookCategory = new List<BookCategory>();
            foreach(var categoryId in categoryIds)
            {
                bookCategory.Add(new BookCategory()
                {
                    BookId = book.Id,
                    CategoryId = categoryId
                });
            }

            await _genericRepositoryBookCategory.AddAllAsync(bookCategory);
            await _genericRepositoryBookCategory.SaveChangesAsync();
        }

        public async Task UpdateAsync(UpdateBookDto updateBookDto, CancellationToken cancellationToken)
        {
            var book = _mapper.Map<UpdateBookDto, Book>(updateBookDto);
            var ids = book.Categories.Select(x => x.CategoryId);
            Dictionary<int, BookCategory> bookCategories = await _bookCategoryRepository.GetByIdAsync(book.Id, cancellationToken);

            foreach (var category in bookCategories)
            {
                if (!ids.Contains(category.Key))
                {
                    await _genericRepositoryBookCategory.DeleteAsync(category.Value);
                }
            }

            _genericRepositoryBook.Update(book);
            await _genericRepositoryBook.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var entity = await _genericRepositoryBook.FindByIdAsync(cancellationToken, id);

            if (entity == null) throw new ArgumentException(ExceptionMessages.EntityNotFound);

            entity.IsRemoved = true;
            _genericRepositoryBook.Update(entity);
            await _genericRepositoryBook.SaveChangesAsync(cancellationToken);
        }
    }
}
