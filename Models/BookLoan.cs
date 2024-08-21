using System.ComponentModel.DataAnnotations;

namespace LibraryMVC.Models
{
    public class BookLoan
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public Book? Book { get; set; }
        public int CustomerId { get; set; }
        public Customer? Customer { get; set; }
        public DateTime LoanDate { get; set; } = DateTime.Now;
        public DateTime? ReturnDate { get; set; }
    }
}
