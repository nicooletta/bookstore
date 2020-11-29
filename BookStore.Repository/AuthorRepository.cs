using BookStore.Domain;
using BookStore.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Repository
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly BookStoreContext context;

        public AuthorRepository(BookStoreContext context)
        {
            this.context = context;
        }

        public async Task<Author> CreateAsync(Author author)
        {
            this.context.Add(author);
            await context.SaveChangesAsync();
            return author;
        }

        public async Task<Author> FindAsync(int id)
        {
            return await context.FindAsync<Author>(id);
        }

        public Author Find(string firstName, string lastName)
        {
            if(string.IsNullOrEmpty(firstName) && string.IsNullOrEmpty(lastName))
            {
                return null;
            }

            return context.Authors.FirstOrDefault(x => x.FirstName.ToLower() == firstName.ToLower() &&
                x.LastName.ToLower() == lastName.ToLower());
        }

        public async Task<IEnumerable<Author>> GetAllAuthorsAsync()
        {
            return await context.Authors.ToListAsync();
        }

        public async Task<Author> FindAuthorAsync(int id)
        {
            return await context.Authors.FirstOrDefaultAsync(x => x.AuthorId == id);
        }
    }
}
