using LibraryMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryMVC.Data
{
    public class LibraryDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BookLoan> BookLoans { get; set; }

        public LibraryDbContext(DbContextOptions<LibraryDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookLoan>()
                .HasOne(bl => bl.Customer)
                .WithMany(c => c.BookLoans)
                .HasForeignKey(bl => bl.CustomerId);

            modelBuilder.Entity<BookLoan>()
                .HasOne(bl => bl.Book)
                .WithMany(b => b.BookLoans)
                .HasForeignKey(bl => bl.BookId);

            // Seed example data
            modelBuilder.Entity<Customer>().HasData(
                new Customer { Id = 1, Name = "John Doe", Email = "john.doe@example.com", PhoneNumber = "0701234567", DateOfBirth = new DateTime(1980, 1, 1) },
                new Customer { Id = 2, Name = "Jane Smith", Email = "jane.smith@example.com", PhoneNumber = "0707654321", DateOfBirth = new DateTime(1990, 5, 15) }
            );

            modelBuilder.Entity<Book>().HasData(
                new Book { Id = 1, Title = "C# Programming", Author = "Author A" },
                new Book { Id = 2, Title = "ASP.NET MVC Guide", Author = "Author B" }
            );
            modelBuilder.Entity<BookLoan>().HasData(
                new BookLoan { Id = 1, BookId = 1, CustomerId = 1, LoanDate = DateTime.Now }
            );
        }
    }
}
