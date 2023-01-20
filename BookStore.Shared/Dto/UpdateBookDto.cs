namespace BookStore.Shared.Dto
{
    public class UpdateBookDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int AuthorId { get; set; }
        public ICollection<UpdateCategoryDto> Categories { get; set; }
    }

    public class UpdateCategoryDto
    {
        public int Id { get; set; }
    }
}
