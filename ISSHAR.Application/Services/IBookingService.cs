using ISSHAR.Application.DTOs.BookingDTOs;

namespace ISSHAR.Application.Services
{
    public interface IBookingService
    {
        Task<ICollection<BookingDisplayDTO>> GetAllAsync();
        Task<BookingDisplayDTO> GetByIdAsync(int id);
        Task<bool> AddAsync(BookingDTO bookingDTO);
        Task<bool> UpdateAsync(int id, BookingDTO bookingDTO);
        Task<bool> DeleteAsync(int id);
        Task<ICollection<BookingDisplayDTO>> GetByHallIdAsync(int hallId);
        Task<ICollection<BookingDisplayDTO>> GetByUserIdAsync(int userId);
        Task<bool> CheckConflictAsync(int hallId, DateTime startDate, DateTime endDate);


    }
}
