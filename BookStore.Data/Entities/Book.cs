using BookStore.Data.Entities.Interfaces;

namespace BookStore.Data.Entities
{
    public class Book : Entity, ITrackable, IRemovable
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int AuthorId { get; set; }
        public Author Author { get; set; }
        public ICollection<BookCategory> Categories { get; set;}
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsRemoved { get; set; }
    }
}
