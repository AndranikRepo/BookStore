namespace BookStore.Data.Entities
{
    public class Category : Entity
    {
        public string Name { get; set; }
        public ICollection<BookCategory> Books { get; set; }
    }
}
