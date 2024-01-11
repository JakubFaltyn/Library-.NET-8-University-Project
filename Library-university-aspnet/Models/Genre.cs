using System.ComponentModel.DataAnnotations;

namespace Library_university_aspnet.Models
{
    public class Genre
    {
        [Key]
        public int GenreId { get; set; }
        public string Name { get; set; }

        // Navigation property
        public List<Book> Books { get; set; }
    }
}
