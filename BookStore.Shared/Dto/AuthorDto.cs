namespace BookStore.Shared.Dto
{
    public class AuthorDto
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public bool IsRemoved { get; set; }
    }
}
