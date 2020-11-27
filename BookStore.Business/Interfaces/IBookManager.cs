using BookStore.Business.DTO;
using BookStore.Domain;
using System.Threading.Tasks;

namespace BookStore.Business.Interfaces
{
    public interface IBookManager
    {
        Task<Book> CreateBook(BookCreateDTO newBook);
    }
}
