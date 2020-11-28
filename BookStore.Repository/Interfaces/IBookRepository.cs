using BookStore.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStore.Repository.Interfaces
{
    public interface IBookRepository
    {
       public Task<Book> AddBookAsync(Book book);
       public Task<Book> UpdateBookAsync(Book book);
       public Task<Book> FindBookAsync(int id);
       public Task<IEnumerable<Book>> GetAllBooksAsync();
       public Task DeleteBookAsync(Book deleteBook);
    }
}
