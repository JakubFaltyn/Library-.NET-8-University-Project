using Library_university_aspnet.Data;
using Library_university_aspnet.Models;
using Library_university_aspnet.Services;
using Microsoft.EntityFrameworkCore;

namespace LibraryServices_tests
{
    public class AuthorServiceTests
    {
        [Fact]
        public async Task AddAuthorAsync_ShouldAddAuthorToDatabase()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "AddAuthorAsync_Database")
                .Options;

            using (var context = new ApplicationDbContext(options))
            {
                var authorService = new AuthorService(context);

                // Act
                await authorService.AddAuthorAsync(new Author { Name = "TestAuthor" });

                // Assert
                Assert.Equal(1, context.Authors.Count());
                Assert.Equal("TestAuthor", context.Authors.First().Name);
            }
        }

        [Fact]
        public async Task CheckAuthorExistenceByName_ShouldReturnTrueIfAuthorExists()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "CheckAuthorExistenceByName_Database")
                .Options;

            using (var context = new ApplicationDbContext(options))
            {
                context.Authors.Add(new Author { Name = "ExistingAuthor" });
                context.SaveChanges();
                var authorService = new AuthorService(context);

                // Act
                var result = await authorService.CheckAuthorExistenceByName("ExistingAuthor");

                // Assert
                Assert.True(result);
            }
        }

        [Fact]
        public async Task CheckAuthorExistenceByName_ShouldReturnFalseIfAuthorDoesNotExist()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "CheckAuthorExistenceByName_Database")
                .Options;

            using (var context = new ApplicationDbContext(options))
            {
                var authorService = new AuthorService(context);

                // Act
                var result = await authorService.CheckAuthorExistenceByName("NonExistingAuthor");

                // Assert
                Assert.False(result);
            }
        }

        [Fact]
        public async Task GetAllAuthorsAsync_ShouldReturnListOfAuthors()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "GetAllAuthorsAsync_Database")
                .Options;

            using (var context = new ApplicationDbContext(options))
            {
                context.Authors.AddRange(
                    new Author { Name = "Author1" },
                    new Author { Name = "Author2" },
                    new Author { Name = "Author3" }
                );
                context.SaveChanges();
                var authorService = new AuthorService(context);

                // Act
                var result = await authorService.GetAllAuthorsAsync();

                // Assert
                Assert.Equal(3, result.Count);
                Assert.Contains(result, a => a.Name == "Author1");
                Assert.Contains(result, a => a.Name == "Author2");
                Assert.Contains(result, a => a.Name == "Author3");
            }
        }

        [Fact]
        public async Task GetAuthorByIdAsync_ShouldReturnAuthorWithGivenId()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "GetAuthorByIdAsync_Database")
                .Options;

            using (var context = new ApplicationDbContext(options))
            {
                context.Authors.AddRange(
                    new Author { AuthorId = 1, Name = "Author1" },
                    new Author { AuthorId = 2, Name = "Author2" },
                    new Author { AuthorId = 3, Name = "Author3" }
                );
                context.SaveChanges();
                var authorService = new AuthorService(context);

                // Act
                var result = await authorService.GetAuthorByIdAsync(2);

                // Assert
                Assert.NotNull(result);
                Assert.Equal(2, result.AuthorId);
                Assert.Equal("Author2", result.Name);
            }
        }

        [Fact]
        public async Task GetAuthorByNameAsync_ShouldReturnAuthorWithGivenName()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "GetAuthorByNameAsync_Database")
                .Options;

            using (var context = new ApplicationDbContext(options))
            {
                context.Authors.AddRange(
                    new Author { Name = "Author1" },
                    new Author { Name = "Author2" },
                    new Author { Name = "Author3" }
                );
                context.SaveChanges();
                var authorService = new AuthorService(context);

                // Act
                var result = await authorService.GetAuthorByNameAsync("Author2");

                // Assert
                Assert.NotNull(result);
                Assert.Equal("Author2", result.Name);
            }
        }

        [Fact]
        public async Task GetAuthorByNameAsync_ShouldThrowExceptionIfAuthorNotFound()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "GetAuthorByNameAsync_Database")
                .Options;

            using (var context = new ApplicationDbContext(options))
            {
                var authorService = new AuthorService(context);

                // Act and Assert
                await Assert.ThrowsAsync<Exception>(() => authorService.GetAuthorByNameAsync("NonExistingAuthor"));
            }
        }
    }
}