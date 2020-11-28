using BookStore.Domain;
using BookStore.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BookStore.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly BookStoreContext context;

        public CustomerRepository(BookStoreContext context)
        {
            this.context = context;
        }

        public async Task<Customer> FindAsync(int id)
        {
            return await context.Customers.Include(x => x.BoughtBooks).FirstOrDefaultAsync(x => x.CustomerId == id);
        }

        public async Task<Customer> PerformPurchese(Customer customer, Book bookStock)
        {
            context.Customers.Update(customer);
            context.Books.Update(bookStock);
            await context.SaveChangesAsync();
            return customer;
        }
    }
}
