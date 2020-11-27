using BookStore.Domain;
using BookStore.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
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

        public async Task<Book> FindBook(int id)
        {
            return await context.Books.Include(x=>x.Author).FirstOrDefaultAsync(x => x.AuthorId == id);
        }

        public async Task<Book> UpdateBookAsync(Book book)
        {
            context.Books.Update(book);
            await context.SaveChangesAsync();
            return book;
        }
    }
}
