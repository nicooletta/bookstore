using BookStore.Domain;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Repository
{
    public class BookStoreContext : DbContext
    {
        public BookStoreContext(DbContextOptions<BookStoreContext> options) : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CustomerBook>().HasKey(bc => new { bc.BookId, bc.CustomerId });
            modelBuilder.Entity<CustomerBook>()
                .HasOne(bc => bc.Book)
                .WithMany(b => b.BookBuyers)
                .HasForeignKey(bc => bc.BookId);
            modelBuilder.Entity<CustomerBook>()
                .HasOne(bc => bc.Customer)
                .WithMany(c => c.BoughtBooks)
                .HasForeignKey(bc => bc.CustomerId);
        }
    }
}
