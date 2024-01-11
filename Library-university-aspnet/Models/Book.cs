using System.ComponentModel.DataAnnotations;

namespace Library_university_aspnet.Models
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }
        public string Title { get; set; }
        public int AuthorId { get; set; }
        public int GenreId { get; set; }

        // Navigation properties
        public Author Author { get; set; }
        public Genre Genre { get; set; }
    }
}
