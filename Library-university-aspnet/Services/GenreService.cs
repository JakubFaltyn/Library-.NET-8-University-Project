using Library_university_aspnet.Data;
using Library_university_aspnet.Models;
using Microsoft.EntityFrameworkCore;

namespace Library_university_aspnet.Services
{
    public class GenreService : IGenreService
    {
        private readonly ApplicationDbContext _context;

        public GenreService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddGenreAsync(Genre genre)
        {
            _context.Genres.Add(genre);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> CheckGenreExistenceByName(string name)
        {
            var genre = await _context.Genres.FirstOrDefaultAsync(g => g.Name == name);

            if (genre != null)
                return true;
            else
                return false;
        }

        public async Task<List<Genre>> GetAllGenresAsync()
        {
            var result = await _context.Genres.ToListAsync();
            return result;
        }

        public async Task<Genre> GetGenreByIdAsync(int id)
        {
            var genre = await _context.Genres.FindAsync(id);

            if (genre != null)
                return genre;
            else
                throw new Exception("Genre not found");
        }

        public async Task<Genre> GetGenreByNameAsync(string name)
        {

            var genre = await _context.Genres.FirstOrDefaultAsync(g => g.Name == name);

            if (genre != null)
                return genre;
            else
                throw new Exception("Genre not found");
        }
    }
}
