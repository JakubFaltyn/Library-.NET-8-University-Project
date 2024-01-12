using Library_university_aspnet.Models;
using Microsoft.AspNetCore.Identity;

namespace Library_university_aspnet.Data
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public virtual ICollection<Reservation> Reservations { get; set; }
    }

}
