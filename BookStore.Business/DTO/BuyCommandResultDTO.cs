namespace BookStore.Business.DTO
{
    public class BuyCommandResultDTO
    {
        public int BookId { get; set; }
        public bool IsStillInStock { get; set; }
        public int CustomerId { get; set; }
        public decimal CustomerNewWallet { get; set; }
    }
}
