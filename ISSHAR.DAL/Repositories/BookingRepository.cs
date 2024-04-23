using ISSHAR.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace ISSHAR.DAL.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly AppDbContext _context;
        public BookingRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<ICollection<Booking>> GetAllAsync()
        {
            return await _context.Bookings.AsNoTracking().ToListAsync();
        }
        public async Task<Booking> GetByIdAsync(int id)
        {
            return await _context.Bookings.AsNoTracking().FirstOrDefaultAsync(a => a.BookingId == id);

        }
        public async Task<bool> AddAsync(Booking booking)
        {
            bool hasConflict = await CheckForConflictsAsync(booking);

            if (hasConflict)
              throw new InvalidOperationException("Booking conflicts with existing booking(s).");

            await _context.Bookings.AddAsync(booking);
            await SaveChangesAsync();

            return true;
        }

        public async Task DeleteAsync(Booking booking)
        {
            _context.Bookings.Remove(booking);
            await SaveChangesAsync();
        }
        public async Task UpdateAsync(Booking booking)
        {
            _context.Bookings.Update(booking);
            await SaveChangesAsync();
        }
        public async Task<ICollection<Booking>> GetByHallIdAsync(int hallId)
        {
            return await _context.Bookings.AsNoTracking().Where(b=>b.HallId==hallId).ToListAsync();

        }
        public async Task<ICollection<Booking>> GetByUserIdAsync(int userId)
        {
            return await _context.Bookings.AsNoTracking().Where(b => b.UserId == userId).ToListAsync();

        }
        public async Task<ICollection<Booking>> GetOverlappingBookingsAsync(int hallId, DateTime startDate, DateTime endDate)
        {
            return await _context.Bookings
                .Where(b => b.HallId == hallId &&
                            (b.StartDate <= endDate && b.EndDate >= startDate))
                .ToListAsync();
        }
        private async Task<bool> CheckForConflictsAsync(Booking newBooking)
        {
            var existingConflictingBookings = await _context.Bookings
                .Where(b =>
                    !(b.EndDate <= newBooking.StartDate || b.StartDate >= newBooking.EndDate))
                .ToListAsync();

            return existingConflictingBookings.Any();
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}