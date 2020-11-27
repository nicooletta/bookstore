using BookStore.Domain;
using System.Threading.Tasks;

namespace BookStore.Repository.Interfaces
{
    public interface IAuthorRepository
    {
      public Task<Author> CreateAsync(Author author);
      public Author Find(string firstName, string lastName);
      public Task<Author> FindAsync(int id);
    }
}
