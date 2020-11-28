using BookStore.Domain;
using BookStore.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly BookStoreContext context;

        public BookRepository(BookStoreContext context)
        {
            this.context = context;
        }

        public async Task<Book> AddBookAsync(Book book)
        {
            context.Books.Add(book);
            await context.SaveChangesAsync();
            return book;
        }

        public async Task<IEnumerable<Book>> GetAllBooksAsync()
        {
            return await context.Books.Include(x=>x.Author).ToListAsync();
        }

        public async Task<Book> FindBookAsync(int id)
        {
            return await context.Books.Include(x => x.Author).FirstOrDefaultAsync(x => x.BookId == id);
        }

        public async Task<Book> UpdateBookAsync(Book book)
        {
            context.Books.Update(book);
            await context.SaveChangesAsync();
            return book;
        }
        public async Task DeleteBookAsync(Book deleteBook)
        {
            context.Remove(deleteBook);
            await context.SaveChangesAsync();
        }
    }
}
