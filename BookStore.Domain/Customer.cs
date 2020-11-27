using System.Collections.Generic;

namespace BookStore.Domain
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public decimal Wallet { get; set; }

        public IEnumerable<CustomerBook> BoughtBooks { get; set; }
    }
}
