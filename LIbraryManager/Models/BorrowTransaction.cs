using System.ComponentModel.DataAnnotations;

namespace LIbraryManager.Models
{
    public class BorrowTransaction
    {
        [Key]
        public int TransactionId { get; set; }

        public int UserId { get; set; }
        public User? User { get; set; }

        public int BookId { get; set; }
        public Book? Book { get; set; }

        public DateTime BorrowDate { get; set; } = DateTime.Now;
        public DateTime DueDate { get; set; } = DateTime.Now.AddDays(7);
        public DateTime? ReturnDate { get; set; }

        [Required]
        public string Status { get; set; } = "Borrowed"; // "Borrowed" or "Returned"
    }
}