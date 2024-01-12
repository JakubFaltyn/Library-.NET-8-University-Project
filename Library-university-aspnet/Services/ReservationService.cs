using Library_university_aspnet.Data;
using Library_university_aspnet.Models;
using Microsoft.EntityFrameworkCore;

namespace Library_university_aspnet.Services
{
    public class ReservationService : IReservationService
    {
        private readonly ApplicationDbContext _context;

        public ReservationService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddReservationAsync(Reservation reservation)
        {
            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> CheckReservationAvailability(int bookId)
        {
            var book = await _context.Books.Include(b => b.Reservations)
                                           .FirstOrDefaultAsync(b => b.BookId == bookId);
            return book != null && !book.Reservations.Any(r => r.IsActive);
        }

        public async Task<List<Reservation>> GetReservationsByUserIdAsync(string userId)
        {
            var reservations = await _context.Reservations
                                             .Where(r => r.UserId == userId)
                                             .ToListAsync();
            return reservations;
        }

        public async Task CancelReservationAsync(int reservationId)
        {
            var reservation = await _context.Reservations.FindAsync(reservationId);
            if (reservation == null)
            {
                throw new Exception("Reservation not found");
            }
            reservation.IsActive = false;
            await _context.SaveChangesAsync();
        }

    }
}
