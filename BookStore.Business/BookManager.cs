using BookStore.Business.DTO;
using BookStore.Business.Exceptions;
using BookStore.Business.Interfaces;
using BookStore.Domain;
using BookStore.Repository.Interfaces;
using System.Threading.Tasks;

namespace BookStore.Business
{
    public class BookManager : IBookManager
    {
        private readonly IBookRepository bookRepository;
        private readonly IAuthorRepository authorRepository;

        public BookManager(IBookRepository bookRepository, IAuthorRepository authorRepository)
        {
            this.bookRepository = bookRepository;
            this.authorRepository = authorRepository;
        }
         
        public async Task<Book> CreateBookAsync(BookCreateDTO newBook)
        {
            var bookToSave = new Book
            {
                Name = newBook.Name,
                Description = newBook.Description,
                ImageUrl = newBook.ImageUrl,
                Price = newBook.Price,
                InStock = newBook.InStock
            };

            var author = await GetOrCreateAuthorAsync(newBook);
            bookToSave.AuthorId = author.AuthorId;
            bookToSave.Author = author;

            var book = await bookRepository.AddBookAsync(bookToSave);
            return book;
        }

        public async Task<Book> UpdateBookAsync(BookUpdateDTO updateBook)
        {
            
            var currentBook = await bookRepository.FindBook(updateBook.BookId);
            bool isAuthorChanged = updateBook.AuthorId != currentBook.AuthorId;
            currentBook.Name = updateBook.Name;
            currentBook.Description = updateBook.Description;
            currentBook.ImageUrl = updateBook.ImageUrl;
            currentBook.Price = updateBook.Price;
            currentBook.InStock = updateBook.InStock;
            currentBook.AuthorId = updateBook.AuthorId;

            var book = await bookRepository.UpdateBookAsync(currentBook);

            if (isAuthorChanged)
            {
                currentBook.Author = await authorRepository.FindAsync(currentBook.AuthorId);
            }

            return book;
        }

        private async Task<Author> GetOrCreateAuthorAsync(BookCreateDTO newBook)
        {
            if (newBook.AuthorId != null)
            {
                var existingAuthor = await authorRepository.FindAsync(newBook.AuthorId.Value);
                if (existingAuthor == null)
                {
                    throw new ValidationFailException("Provided author does not exists in our list");
                }
                return existingAuthor;
            }

            var author = authorRepository.Find(newBook.AuthorFirstName, newBook.AuthorLastName);
            if (author != null)
            {
                return author;
            }

            var newAuthor = await authorRepository.CreateAsync(new Author { FirstName = newBook.AuthorFirstName, LastName = newBook.AuthorLastName });
            return newAuthor;
        }
    }
}
