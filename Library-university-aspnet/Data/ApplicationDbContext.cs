using Library_university_aspnet.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Library_university_aspnet.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Define Author-Book relationship
            modelBuilder.Entity<Book>()
                .HasOne(b => b.Author)
                .WithMany(a => a.Books)
                .HasForeignKey(b => b.AuthorId);

            // Define Genre-Book relationship
            modelBuilder.Entity<Book>()
                .HasOne(b => b.Genre)
                .WithMany(g => g.Books)
                .HasForeignKey(b => b.GenreId);


            // Seed example data
            modelBuilder.Entity<Author>().HasData(
                new Author { AuthorId = 1, Name = "John Doe" },
                new Author { AuthorId = 2, Name = "Jane Smith" }
            );

            modelBuilder.Entity<Genre>().HasData(
                new Genre { GenreId = 1, Name = "Science Fiction" },
                new Genre { GenreId = 2, Name = "Mystery" }
            );

            modelBuilder.Entity<Book>().HasData(
                new Book { BookId = 1, Title = "The Future", AuthorId = 1, GenreId = 1 },
                new Book { BookId = 2, Title = "Secrets Unveiled", AuthorId = 2, GenreId = 2 }
            );

        }
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
    }
}
