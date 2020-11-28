namespace BookStore.Models
{
    public class BuyResultViewModel
    {
        public int BookId { get; set; }
        public bool IsStillInStock { get; set; }
        public int CustomerId { get; set; }
        public decimal CustomerNewWallet { get; set; }
    }
}
