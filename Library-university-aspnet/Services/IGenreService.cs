using Library_university_aspnet.Models;

namespace Library_university_aspnet.Services
{
    public interface IGenreService
    {
        Task<List<Genre>> GetAllGenresAsync();
        Task<Genre> GetGenreByNameAsync(string name);
        Task<Genre> GetGenreByIdAsync(int id);
        Task AddGenreAsync(Genre genre);
        Task<bool> CheckGenreExistenceByName(string name);
    }
}
