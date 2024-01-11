using Library_university_aspnet.Models;

namespace Library_university_aspnet.Services
{
    public interface IBookService
    {
        Task<List<Book>> GetAllBooksAsync();
        Task<Book> GetBookByIdAsync(int id);

        Task AddBookAsync(Book book);
        Task UpdateBookAsync(Book book, int id);
        Task DeleteBookAsync(int bookId);
    }
}
