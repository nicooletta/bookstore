﻿using BookStore.Domain;
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
    }
}