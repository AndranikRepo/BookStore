namespace BookStore.Data.Entities.Interfaces
{
    public interface IHasKey<TKey>
    {
        TKey Id { get; set; }
    }
}
