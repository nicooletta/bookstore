namespace BookStore.Domain
{
    public class CustomerBook
    {
        public int BookId { get; set; }
        public int CustomerId { get; set; }
        public Book Book { get; set; }
        public Customer Customer { get; set; }
    }
}
