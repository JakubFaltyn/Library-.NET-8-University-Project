using System.ComponentModel.DataAnnotations;

namespace Library_university_aspnet.Models
{
    public class Author
    {
        [Key]
        public int AuthorId { get; set; }

        public string Name { get; set; }

        // Navigation property
        public List<Book> Books { get; set; }
    }
}
