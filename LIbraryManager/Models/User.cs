using System.ComponentModel.DataAnnotations;

namespace LIbraryManager.Models
{
    public class User
    {
        public int UserId { get; set; }

        [Required, StringLength(100)]
        public string FullName { get; set; } = string.Empty;

        [Required, StringLength(30)]
        public string StudentNumber { get; set; } = string.Empty;

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        [Required]
        public string Role { get; set; } = "Member"; // "Librarian" or "Member"

        public ICollection<BorrowTransaction> BorrowTransactions { get; set; } = new List<BorrowTransaction>();
    }
}