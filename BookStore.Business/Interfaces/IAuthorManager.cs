using BookStore.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStore.Business.Interfaces
{
    public interface IAuthorManager
    {
        Task<IEnumerable<Author>> GetAllAuthorsAsync();
        Task<Author> GetAuthorAsync(int id);
    }
}
