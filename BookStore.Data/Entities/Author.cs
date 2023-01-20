using BookStore.Data.Entities.Interfaces;

namespace BookStore.Data.Entities
{
    public class Author : Entity, ITrackable, IRemovable
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public ICollection<Book> Books { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsRemoved { get; set; }
    }
}
