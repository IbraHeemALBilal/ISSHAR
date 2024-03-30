using ISSHAR.DAL.Entities;

namespace ISSHAR.DAL.Repositories
{
    public interface IBookingRepository
    {
        Task<ICollection<Booking>> GetAllAsync();
        Task<Booking> GetByIdAsync(int id);
        Task AddAsync(Booking booking);
        Task DeleteAsync(Booking booking);
        Task SaveChangesAsync();
    }
}
