using LIbraryManager.Data;
using LIbraryManager.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LIbraryManager.Controllers
{
    [Authorize]
    public class BooksController : Controller
    {
        private readonly LibraryDbContext _context;

        public BooksController(LibraryDbContext context)
        {
            _context = context;
        }

        // READ: List all books (with optional search)
        public async Task<IActionResult> Index(string searchString)
        {
            var books = from b in _context.Books select b;

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                books = books.Where(b => b.Title.Contains(searchString)
                                      || b.Author.Contains(searchString)
                                      || b.ISBN.Contains(searchString)
                                      || b.Category.Contains(searchString));
            }

            ViewBag.CurrentFilter = searchString;
            return View(await books.ToListAsync());
        }

        // READ: Book details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var book = await _context.Books.FirstOrDefaultAsync(m => m.BookId == id);
            if (book == null) return NotFound();

            return View(book);
        }

        // CREATE (GET) - Librarian only
        [Authorize(Roles = "Librarian")]
        public IActionResult Create() => View();

        // CREATE (POST) - Librarian only
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Librarian")]
        public async Task<IActionResult> Create(Book book)
        {
            if (ModelState.IsValid)
            {
                book.AvailableCopies = book.TotalCopies; // Start with all copies available
                _context.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        // UPDATE (GET) - Librarian only
        [Authorize(Roles = "Librarian")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var book = await _context.Books.FindAsync(id);
            if (book == null) return NotFound();

            return View(book);
        }

        // UPDATE (POST) - Librarian only
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Librarian")]
        public async Task<IActionResult> Edit(int id, Book book)
        {
            if (id != book.BookId) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        // DELETE (GET) - Librarian only
        [Authorize(Roles = "Librarian")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var book = await _context.Books.FirstOrDefaultAsync(m => m.BookId == id);
            if (book == null) return NotFound();

            return View(book);
        }

        // DELETE (POST) - Librarian only
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Librarian")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book != null)
            {
                _context.Books.Remove(book);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}