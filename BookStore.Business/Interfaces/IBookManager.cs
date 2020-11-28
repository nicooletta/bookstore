using BookStore.Business.DTO;
using BookStore.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStore.Business.Interfaces
{
    public interface IBookManager
    {
        Task<Book> CreateBookAsync(BookCreateDTO newBook);
        Task<Book> UpdateBookAsync(int bookId, BookUpdateDTO updateBook);
        Task DeleteBookAsync(int id);
        Task<Book> GetBookAsync(int id);
        Task<IEnumerable<Book>> GetAllBooksAsync();
    }
}
