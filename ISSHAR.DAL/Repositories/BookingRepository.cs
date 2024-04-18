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
            return await _context.Bookings.AsNoTracking().Where(b=>b.HallId==hallId).ToListAsync();

        }
        public async Task<ICollection<Booking>> GetByUserIdAsync(int userId)
        {
            return await _context.Bookings.AsNoTracking().Where(b => b.UserId == userId).ToListAsync();

        }


        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}