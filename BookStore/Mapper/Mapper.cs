using BookStore.Models;

namespace BookStore.Mapper
{
    public static class BookMapper
    {
        public static BookViewModel MapBook(Domain.Book createdBook)
        {
            return new BookViewModel
            {
                Author = createdBook.Author.FirstName + " " + createdBook.Author.LastName,
                BookId = createdBook.BookId,
                Description = createdBook.Description,
                ImageUrl = createdBook.ImageUrl,
                IsInStock = createdBook.InStock > 0,
                Name = createdBook.Name,
                Price = createdBook.Price
            };
        }
    }
}
