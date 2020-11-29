using BookStore.Business.Interfaces;
using BookStore.Domain;
using BookStore.Repository.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStore.Business
{
    public class AuthorManager : IAuthorManager
    {
        private readonly IAuthorRepository authorRepository;

        public AuthorManager(IAuthorRepository authorRepository)
        {
            this.authorRepository = authorRepository;
        }

        public async Task<IEnumerable<Author>> GetAllAuthorsAsync()
        {
            return await authorRepository.GetAllAuthorsAsync();
        }  

        public async Task<Author> GetAuthorAsync(int id)
        {
            return await authorRepository.FindAuthorAsync(id);
        }      
    }
}
