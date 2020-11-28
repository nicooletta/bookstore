using BookStore.Domain;
using System.Threading.Tasks;

namespace BookStore.Repository.Interfaces
{
    public interface ICustomerRepository
    {
        public Task<Customer> FindAsync(int id);
        Task<Customer> PerformPurchese(Customer customer, Book book);
    }
}
