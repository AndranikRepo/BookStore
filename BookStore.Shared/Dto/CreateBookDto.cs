namespace BookStore.Shared.Dto
{
    public class CreateBookDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int AuthorId { get; set; }
        public IList<int> CategoryIds { get; set; }
    }
}
