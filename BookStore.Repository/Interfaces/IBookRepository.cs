using BookStore.Domain;
using System.Threading.Tasks;

namespace BookStore.Repository.Interfaces
{
    public interface IBookRepository
    {
       public Task<Book> AddBookAsync(Book book);
    }
}
