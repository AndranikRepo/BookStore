namespace BookStore.Shared.Dto
{
    public class BookDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public AuthorDto Author { get; set; }
        public ICollection<CategoryDto> Categories { get; set; }
    }
}
