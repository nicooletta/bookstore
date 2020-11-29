using BookStore.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStore.Repository.Interfaces
{
    public interface IAuthorRepository
    {
      public Task<Author> CreateAsync(Author author);
      public Author Find(string firstName, string lastName);
      public Task<Author> FindAsync(int id);
      public Task<IEnumerable<Author>> GetAllAuthorsAsync();
        Task<Author> FindAuthorAsync(int id);
    }
}
