using Library_university_aspnet.Data;
using Library_university_aspnet.Models;
using Library_university_aspnet.Services;
using Microsoft.EntityFrameworkCore;
namespace LibraryServices_tests
{

    public class GenreServiceTests
    {
        [Fact]
        public async Task AddGenreAsync_ShouldAddGenreToDatabase()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "AddGenreAsync_Database")
                .Options;

            using (var context = new ApplicationDbContext(options))
            {
                var genreService = new GenreService(context);

                // Act
                await genreService.AddGenreAsync(new Genre { Name = "TestGenre" });

                // Assert
                Assert.Equal(1, context.Genres.Count());
                Assert.Equal("TestGenre", context.Genres.First().Name);
            }
        }

        [Fact]
        public async Task CheckGenreExistenceByName_ShouldReturnTrueIfGenreExists()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "CheckGenreExistenceByName_Database")
                .Options;

            using (var context = new ApplicationDbContext(options))
            {
                context.Genres.Add(new Genre { Name = "ExistingGenre" });
                context.SaveChanges();
                var genreService = new GenreService(context);

                // Act
                var result = await genreService.CheckGenreExistenceByName("ExistingGenre");

                // Assert
                Assert.True(result);
            }
        }

        [Fact]
        public async Task CheckGenreExistenceByName_ShouldReturnFalseIfGenreDoesNotExist()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "CheckGenreExistenceByName_Database")
                .Options;

            using (var context = new ApplicationDbContext(options))
            {
                var genreService = new GenreService(context);

                // Act
                var result = await genreService.CheckGenreExistenceByName("NonExistingGenre");

                // Assert
                Assert.False(result);
            }
        }

        [Fact]
        public async Task GetAllGenresAsync_ShouldReturnListOfGenres()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "GetAllGenresAsync_Database")
                .Options;

            using (var context = new ApplicationDbContext(options))
            {
                context.Genres.AddRange(
                    new Genre { Name = "Genre1" },
                    new Genre { Name = "Genre2" },
                    new Genre { Name = "Genre3" }
                );
                context.SaveChanges();
                var genreService = new GenreService(context);

                // Act
                var result = await genreService.GetAllGenresAsync();

                // Assert
                Assert.Equal(3, result.Count);
                Assert.Contains(result, g => g.Name == "Genre1");
                Assert.Contains(result, g => g.Name == "Genre2");
                Assert.Contains(result, g => g.Name == "Genre3");
            }
        }

        [Fact]
        public async Task GetGenreByIdAsync_ShouldReturnGenreWithGivenId()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "GetGenreByIdAsync_Database")
                .Options;

            using (var context = new ApplicationDbContext(options))
            {
                context.Genres.AddRange(
                    new Genre { GenreId = 1, Name = "Genre1" },
                    new Genre { GenreId = 2, Name = "Genre2" },
                    new Genre { GenreId = 3, Name = "Genre3" }
                );
                context.SaveChanges();
                var genreService = new GenreService(context);

                // Act
                var result = await genreService.GetGenreByIdAsync(2);

                // Assert
                Assert.NotNull(result);
                Assert.Equal(2, result.GenreId);
                Assert.Equal("Genre2", result.Name);
            }
        }

        [Fact]
        public async Task GetGenreByIdAsync_ShouldThrowExceptionIfGenreNotFound()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "GetGenreByIdAsync_Database")
                .Options;

            using (var context = new ApplicationDbContext(options))
            {
                var genreService = new GenreService(context);

                // Act and Assert
                await Assert.ThrowsAsync<Exception>(() => genreService.GetGenreByIdAsync(1));
            }
        }

        [Fact]
        public async Task GetGenreByNameAsync_ShouldReturnGenreWithGivenName()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "GetGenreByNameAsync_Database")
                .Options;

            using (var context = new ApplicationDbContext(options))
            {
                context.Genres.AddRange(
                    new Genre { Name = "Genre1" },
                    new Genre { Name = "Genre2" },
                    new Genre { Name = "Genre3" }
                );
                context.SaveChanges();
                var genreService = new GenreService(context);

                // Act
                var result = await genreService.GetGenreByNameAsync("Genre2");

                // Assert
                Assert.NotNull(result);
                Assert.Equal("Genre2", result.Name);
            }
        }

        [Fact]
        public async Task GetGenreByNameAsync_ShouldThrowExceptionIfGenreNotFound()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "GetGenreByNameAsync_Database")
                .Options;

            using (var context = new ApplicationDbContext(options))
            {
                var genreService = new GenreService(context);

                // Act and Assert
                await Assert.ThrowsAsync<Exception>(() => genreService.GetGenreByNameAsync("NonExistingGenre"));
            }
        }
    }
}
