using System.ComponentModel.DataAnnotations;

namespace LIbraryManager.Models
{
    public class Book
    {
        public int BookId { get; set; }

        [Required, StringLength(20)]
        public string ISBN { get; set; } = string.Empty;

        [Required, StringLength(150)]
        public string Title { get; set; } = string.Empty;

        [Required, StringLength(100)]
        public string Author { get; set; } = string.Empty;

        [Required, StringLength(50)]
        public string Category { get; set; } = string.Empty;

        [Range(1, 1000)]
        public int TotalCopies { get; set; }

        [Range(0, 1000)]
        public int AvailableCopies { get; set; }

        public ICollection<BorrowTransaction> BorrowTransactions { get; set; } = new List<BorrowTransaction>();
    }
}
