using Library_university_aspnet.Data;
using Library_university_aspnet.Models;
using Microsoft.EntityFrameworkCore;

namespace Library_university_aspnet.Services
{
    public class BookService : IBookService
    {
        private readonly ApplicationDbContext _context;

        public BookService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddBookAsync(Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteBookAsync(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book != null)
            {
                _context.Books.Remove(book);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Book>> GetAllBooksAsync()
        {
            var result = await _context.Books
                .Include(b => b.Author)
                .Include(b => b.Genre)
                .ToListAsync();
            return result;
        }

        public async Task<Book> GetBookByIdAsync(int id)
        {
            var book = await _context.Books.FindAsync(id);

            if (book != null)
                return book;
            else
                throw new Exception("Book not found");
        }

        public async Task UpdateBookAsync(Book book, int id)
        {
            var dbBook = await _context.Books.FindAsync(id);
            if (book != null)
            {
                dbBook.Title = book.Title;
                dbBook.AuthorId = book.AuthorId;
                dbBook.GenreId = book.GenreId;
                dbBook.Author = book.Author;
                dbBook.Genre = book.Genre;

                await _context.SaveChangesAsync();
            }
        }
    }
}
