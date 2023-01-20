using BookStore.Data.Entities.Interfaces;

namespace BookStore.Data.Entities
{
    public class BaseEntity<TKey> : IHasKey<TKey>
    {
        public TKey Id { get; set; }
    }
}
