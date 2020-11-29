using BookStore.Domain;
using BookStore.Models;

namespace BookStore.Mapper
{
    public static class Mapper
    {
        public static BookViewModel MapBook(Book sourcebook)
        {
            return new BookViewModel
            {
                Author = sourcebook.Author.FirstName + " " + sourcebook.Author.LastName,
                BookId = sourcebook.BookId,
                Description = sourcebook.Description,
                ImageUrl = sourcebook.ImageUrl,
                IsInStock = sourcebook.InStock > 0,
                Name = sourcebook.Name,
                Price = sourcebook.Price
            };
        }

        public static AuthorViewModel MapAuthor(Author sourcebook)
        {
            return new AuthorViewModel
            {
                FirstName = sourcebook.FirstName,
                LastName = sourcebook.LastName
            };
        }
    }
}
