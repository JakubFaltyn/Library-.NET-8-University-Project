using Library_university_aspnet.Models;

namespace Library_university_aspnet.Services
{
    public interface IReservationService
    {
        Task AddReservationAsync(Reservation reservation);
        Task<bool> CheckReservationAvailability(int bookId);
        Task<List<Reservation>> GetReservationsByUserIdAsync(string userId);
        Task CancelReservationAsync(int reservationId);
    }
}
