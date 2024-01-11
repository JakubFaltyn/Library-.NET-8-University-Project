using Library_university_aspnet.Data;
using Library_university_aspnet.Models;
using Microsoft.EntityFrameworkCore;

namespace Library_university_aspnet.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly ApplicationDbContext _context;

        public AuthorService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Author>> GetAllAuthorsAsync()
        {
            var result = await _context.Authors.ToListAsync();
            return result;
        }

        public async Task AddAuthorAsync(Author author)
        {
            _context.Authors.Add(author);
            await _context.SaveChangesAsync();
        }


        public async Task<bool> CheckAuthorExistenceByName(string name)
        {
            var author = await _context.Authors.FirstOrDefaultAsync(a => a.Name == name);

            if (author != null)
                return true;
            else
                return false;
        }

        public async Task<Author> GetAuthorByNameAsync(string name)
        {
            var author = await _context.Authors.FirstOrDefaultAsync(a => a.Name == name);

            if (author != null)
                return author;
            else
                throw new Exception("Author not found");
        }

        public async Task<Author> GetAuthorByIdAsync(int id)
        {
            var author = await _context.Authors.FindAsync(id);

            if (author != null)
                return author;
            else
                throw new Exception("Author not found");
        }

    }
}
