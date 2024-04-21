using ISSHAR.DAL.Entities;

namespace ISSHAR.DAL.Repositories
{
    public interface IBookingRepository
    {
        Task<ICollection<Booking>> GetAllAsync();
        Task<Booking> GetByIdAsync(int id);
        Task AddAsync(Booking booking);
        Task UpdateAsync(Booking booking);
        Task DeleteAsync(Booking booking);
        Task<ICollection<Booking>> GetByHallIdAsync(int hallId);
        Task<ICollection<Booking>> GetByUserIdAsync(int userId);
        Task<ICollection<Booking>> GetOverlappingBookingsAsync(int hallId, DateTime startDate, DateTime endDate);
        Task SaveChangesAsync();
    }
}
