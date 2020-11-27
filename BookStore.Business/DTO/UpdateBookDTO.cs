using System.ComponentModel.DataAnnotations;

namespace BookStore.Business.DTO
{
    public class BookUpdateDTO
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Incorrect book id")]
        public int BookId { get; set; }

        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public int AuthorId { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be positive value")]
        public decimal Price { get; set; }

        public string ImageUrl { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int InStock { get; set; }
    }
}
