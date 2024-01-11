using Library_university_aspnet.Models;

namespace Library_university_aspnet.Services
{
    public interface IAuthorService
    {
        Task<List<Author>> GetAllAuthorsAsync();
        Task<Author> GetAuthorByNameAsync(string name);
        Task<Author> GetAuthorByIdAsync(int id);
        Task AddAuthorAsync(Author author);
        Task<bool> CheckAuthorExistenceByName(string name);
    }
}
