using BookStore.Business.DTO;
using BookStore.Business.Exceptions;
using BookStore.Business.Interfaces;
using BookStore.Domain;
using BookStore.Repository.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Business
{
    public class BookManager : IBookManager
    {
        private readonly IBookRepository bookRepository;
        private readonly IAuthorRepository authorRepository;
        private readonly ICustomerRepository customerRepository;

        public BookManager(IBookRepository bookRepository, IAuthorRepository authorRepository, ICustomerRepository customerRepository)
        {
            this.bookRepository = bookRepository;
            this.authorRepository = authorRepository;
            this.customerRepository = customerRepository;
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

        public async Task<Book> UpdateBookAsync(int bookId, BookUpdateDTO updateBook)
        {            
            var currentBook = await bookRepository.FindBookAsync(bookId);
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

        public async Task DeleteBookAsync(int id)
        {
            var deleteBook = await bookRepository.FindBookAsync(id);
            if (deleteBook == null)
            {
                throw new ResourceNotFoundException($"Provided Book with id {id} does not exist");
            }
            await bookRepository.DeleteBookAsync(deleteBook);
        }

        public async Task<Book> GetBookAsync(int id)
        {
           return await bookRepository.FindBookAsync(id);
        }

        public async Task<IEnumerable<Book>> GetAllBooksAsync()
        {
            return await bookRepository.GetAllBooksAsync();
        }

        public async Task<BuyCommandResultDTO> SellBook(int bookId, int customerId)
        {
            var buyer = await customerRepository.FindAsync(customerId);
            if(buyer.BoughtBooks.Any(x=>x.BookId == bookId))
            {
                throw new ValidationFailException("Your already own this book");
            }

            var bookStock = await bookRepository.FindBookAsync(bookId);
            if (bookStock == null)
            {
                throw new ResourceNotFoundException($"Provided Book with id {bookId} does not exist");
            }
            if (buyer.Wallet < bookStock.Price)
            {
                throw new ValidationFailException("You don't have enough funds to buy this book. Please add money to your BookStore wallet.");
            }

            buyer.BoughtBooks.Add(new CustomerBook { BookId = bookId, CustomerId = customerId });
            buyer.Wallet = buyer.Wallet - bookStock.Price;
            bookStock.InStock--;
            
            await customerRepository.PerformPurchese(buyer, bookStock);

            return new BuyCommandResultDTO
            {
                BookId = bookStock.BookId,
                IsStillInStock = bookStock.InStock > 0,
                CustomerId = buyer.CustomerId,
                CustomerNewWallet = buyer.Wallet
            };
        }

        private async Task<Author> GetOrCreateAuthorAsync(BookCreateDTO newBook)
        {
            if (newBook.AuthorId != null)
            {
                var existingAuthor = await authorRepository.FindAsync(newBook.AuthorId.Value);
                if (existingAuthor == null)
                {
                    throw new ResourceNotFoundException("Provided author does not exists in our list");
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
