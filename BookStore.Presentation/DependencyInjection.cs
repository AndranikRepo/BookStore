using BookStore.Data.Entities;
using BookStore.Data.Repositories.Classes;
using BookStore.Data.Repositories.Interfaces;
using BookStore.Infrastructure.Classes;
using BookStore.Infrastructure.Interfaces;
using BookStore.Presentation.AutoMapper;

namespace BookStore.Presentation
{
    public static class DependencyInjection
    {
        public static void AddDI(this IServiceCollection services)
        {
            services.AddTransient<IGenericRepository<Book>, GenericRepository<Book>>();
            services.AddTransient<IGenericRepository<Category>, GenericRepository<Category>>();
            services.AddTransient<IGenericRepository<BookCategory>, GenericRepository<BookCategory>>();
            services.AddTransient<IGenericRepository<Author>, GenericRepository<Author>>();
            services.AddTransient<IBookRepository, BookRepository>();
            services.AddTransient<IBookService, BookService>();
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<IAuthorService, AuthorService>();
            services.AddTransient<IAuthorRepository, AuthorRepository>();
            services.AddTransient<IBookCategoryRepository, BookCategoryRepository>();

            services.AddAutoMapper(typeof(AutoMapperProfile));
        }
    }
}
