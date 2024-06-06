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
            return await _context.Bookings.AsNoTracking().Include(c => c.Hall).Include(b=>b.User).FirstOrDefaultAsync(a => a.BookingId == id);

        }
        public async Task AddAsync(Booking booking)
        {
            await _context.Bookings.AddAsync(booking);
            await SaveChangesAsync();
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
            return await _context.Bookings.AsNoTracking().Include(b=> b.User).Where(b=>b.HallId==hallId).ToListAsync();
        }
        public async Task<ICollection<Booking>> GetByHallIdAndDateAsync(int hallId, DateOnly date)
        {
            return await _context.Bookings.AsNoTracking()
                .Include(b => b.User)
                .Where(b => b.HallId == hallId && b.StartDate.Date == date.ToDateTime(TimeOnly.MinValue).Date)
                .ToListAsync();
        }
        public async Task<ICollection<Booking>> GetByUserIdAsync(int userId)
        {
            return await _context.Bookings.AsNoTracking().Include(b=>b.Hall).Where(b => b.UserId == userId).ToListAsync();

        }
        public async Task<bool> HasBookingConflictAsync(int hallId, DateTime startDate, DateTime endDate)
        {
            var isConflict = await _context.Bookings
                .AnyAsync(b => b.HallId == hallId &&
                               !(b.EndDate <= startDate || b.StartDate >= endDate));

            return isConflict;
        }
        public async Task<bool> HasFutureBookingsAsync(int hallId)
        {
            var now = DateTime.Now;

            var hasFutureBookings = await _context.Bookings
                .AnyAsync(b => b.HallId == hallId && b.EndDate > now);

            return hasFutureBookings;
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}