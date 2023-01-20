using BookStore.Data.Entities;
using BookStore.Data.Entities.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStore.Data.Contexts
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<BookCategory> BookCategories { get; set; }
        public DbSet<Author> Authors { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach(var entry in this.ChangeTracker.Entries().Where(x => x.State == EntityState.Added || x.State == EntityState.Modified))
            {
                if (entry.Entity is ITrackable entity)
                {
                    if (entry.State == EntityState.Added)
                        entity.CreatedAt = DateTime.Now;

                    entity.UpdatedAt = DateTime.Now;
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>(ConfigureBook);
            modelBuilder.Entity<Category>(ConfigureCategory);
            modelBuilder.Entity<BookCategory>(ConfigureBookCategory);
            modelBuilder.Entity<Author>(ConfigureAuthor);
        }

        private void ConfigureBook(EntityTypeBuilder<Book> entityTypeBuilder)
        {
            entityTypeBuilder.ToTable("Books");
            entityTypeBuilder.HasKey(x => x.Id);
            entityTypeBuilder.Property(x => x.Title).IsRequired();
            entityTypeBuilder.HasOne(x => x.Author).WithMany(x => x.Books).HasForeignKey(x => x.AuthorId);
        }

        private void ConfigureCategory(EntityTypeBuilder<Category> entityTypeBuilder) 
        {
            entityTypeBuilder.ToTable("Categories");
            entityTypeBuilder.HasKey(x => x.Id);
            entityTypeBuilder.HasAlternateKey(x => x.Name);
        }

        private void ConfigureBookCategory(EntityTypeBuilder<BookCategory> entityTypeBuilder)
        {
            entityTypeBuilder.ToTable("BookCategory");
            entityTypeBuilder.HasKey(x => new { x.BookId, x.CategoryId });
            entityTypeBuilder.HasOne(x => x.Book).WithMany(x => x.Categories).HasForeignKey(x => x.BookId);
            entityTypeBuilder.HasOne(x => x.Category).WithMany(x => x.Books).HasForeignKey(x => x.CategoryId);
        }

        private void ConfigureAuthor(EntityTypeBuilder<Author> entityTypeBuilder)
        {
            entityTypeBuilder.ToTable("Authors");
            entityTypeBuilder.HasKey(x => x.Id);
            entityTypeBuilder.Property(x => x.Firstname).IsRequired();
            entityTypeBuilder.Property(x => x.Lastname).IsRequired();
        }
    }
}
