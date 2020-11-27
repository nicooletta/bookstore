using System;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Business.DTO
{
    public class BookCreateDTO
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }

        public string AuthorFirstName { get; set; }
        public string AuthorLastName { get; set; }
        public int? AuthorId { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be positive value")]
        public decimal Price { get; set; }

        public string ImageUrl { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int InStock { get; set; }
    }
}
