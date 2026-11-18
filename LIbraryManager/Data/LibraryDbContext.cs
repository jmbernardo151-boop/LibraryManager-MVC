using LIbraryManager.Models;
using Microsoft.EntityFrameworkCore;

namespace LIbraryManager.Data
{
    public class LibraryDbContext : DbContext
    {
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BorrowTransaction> BorrowTransactions { get; set; }
    }
}