using Library_university_aspnet.Data;
using System.ComponentModel.DataAnnotations;

namespace Library_university_aspnet.Models
{
    public class Reservation
    {
        [Key]
        public int ReservationId { get; set; }
        public string UserId { get; set; }
        public int BookId { get; set; }
        public DateTime ReservationDate { get; set; }
        public bool IsActive { get; set; }

        // Navigation properties
        public virtual ApplicationUser User { get; set; }
        public virtual Book Book { get; set; }
    }
}