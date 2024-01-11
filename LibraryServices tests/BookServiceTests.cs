using Library_university_aspnet.Data;
using Library_university_aspnet.Models;
using Library_university_aspnet.Services;
using Microsoft.EntityFrameworkCore;

namespace LibraryServices_tests
{
    public class BookServiceTests
    {
        [Fact]
        public async Task GetAllBooksAsync_ReturnsAllBooks()
        {
            // Arrange
            var dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "InMemoryDb")
                .Options;

            using (var context = new ApplicationDbContext(dbContextOptions))
            {
                var author = new Author { Name = "Author1" };
                var genre = new Genre { Name = "Genre1" };

                context.Authors.Add(author);
                context.Genres.Add(genre);

                context.Books.Add(new Book { Title = "Book1", AuthorId = author.AuthorId, GenreId = genre.GenreId });
                context.Books.Add(new Book { Title = "Book2", AuthorId = author.AuthorId, GenreId = genre.GenreId });
                context.SaveChanges();
            }

            using (var context = new ApplicationDbContext(dbContextOptions))
            {
                var bookService = new BookService(context);

                // Act
                var books = await bookService.GetAllBooksAsync();

                // Assert
                Assert.Equal(2, books.Count);
                Assert.Equal("Book1", books[0].Title);
                Assert.Equal("Book2", books[1].Title);
            }
        }

        [Fact]
        public async Task AddBookAsync_AddsBookToDatabase()
        {
            // Arrange
            var dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "InMemoryDb")
                .Options;

            using (var context = new ApplicationDbContext(dbContextOptions))
            {
                var bookService = new BookService(context);
                var author = new Author { Name = "Author1" };
                var genre = new Genre { Name = "Genre1" };

                context.Authors.Add(author);
                context.Genres.Add(genre);
                context.SaveChanges();

                var newBook = new Book { Title = "NewBook", AuthorId = author.AuthorId, GenreId = genre.GenreId };

                // Act
                await bookService.AddBookAsync(newBook);

                // Assert
                var addedBook = await context.Books.FirstOrDefaultAsync(b => b.Title == "NewBook");
                Assert.NotNull(addedBook);
            }
        }

        [Fact]
        public async Task DeleteBookAsync_RemovesBookFromDatabase()
        {
            // Arrange
            var dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "InMemoryDb")
                .Options;

            using (var context = new ApplicationDbContext(dbContextOptions))
            {
                var bookService = new BookService(context);
                var author = new Author { Name = "Author1" };
                var genre = new Genre { Name = "Genre1" };

                context.Authors.Add(author);
                context.Genres.Add(genre);
                context.Books.Add(new Book { Title = "BookToDelete", AuthorId = author.AuthorId, GenreId = genre.GenreId });
                context.SaveChanges();

                // Act
                await bookService.DeleteBookAsync(1);

                // Assert
                var deletedBook = await context.Books.FindAsync(1);
                Assert.Null(deletedBook);
            }
        }

        [Fact]
        public async Task GetBookByIdAsync_ReturnsBookById()
        {
            // Arrange
            var dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "InMemoryDb")
                .Options;

            using (var context = new ApplicationDbContext(dbContextOptions))
            {
                var bookService = new BookService(context);
                var author = new Author { Name = "Author1" };
                var genre = new Genre { Name = "Genre1" };

                context.Authors.Add(author);
                context.Genres.Add(genre);
                context.Books.Add(new Book { Title = "Book1", AuthorId = author.AuthorId, GenreId = genre.GenreId });
                context.SaveChanges();
            }

            using (var context = new ApplicationDbContext(dbContextOptions))
            {
                var bookService = new BookService(context);

                // Act
                var book = await bookService.GetBookByIdAsync(1);

                // Assert
                Assert.NotNull(book);
                Assert.Equal("Book1", book.Title);
            }
        }


        [Fact]
        public async Task UpdateBookAsync_UpdatesBookInDatabase()
        {
            // Arrange
            var dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "InMemoryDb")
                .Options;

            using (var context = new ApplicationDbContext(dbContextOptions))
            {
                var bookService = new BookService(context);
                var author = new Author { Name = "Author1" };
                var genre = new Genre { Name = "Genre1" };

                context.Authors.Add(author);
                context.Genres.Add(genre);
                context.Books.Add(new Book { BookId = 1, Title = "Book1", AuthorId = author.AuthorId, GenreId = genre.GenreId });
                context.SaveChanges();
            }

            using (var context = new ApplicationDbContext(dbContextOptions))
            {
                var bookService = new BookService(context);
                var updatedBook = new Book { Title = "UpdatedBook", AuthorId = 1, GenreId = 1 };

                // Act
                await bookService.UpdateBookAsync(updatedBook, 1);

                // Assert
                var book = await context.Books.FindAsync(1);
                Assert.NotNull(book);
                Assert.Equal("UpdatedBook", book.Title);
            }
        }
    }
}
