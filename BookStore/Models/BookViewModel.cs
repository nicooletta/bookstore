namespace BookStore.Models
{
    public class BookViewModel
    {
        public int BookId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public bool InStock { get; set; }
    }
}
