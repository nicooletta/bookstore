using System.Collections.Generic;

namespace BookStore.Domain
{
    public class Book
    {
        public int BookId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int AuthorId { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public int InStock { get; set; }
        public Author Author { get; set; }
    }
}
