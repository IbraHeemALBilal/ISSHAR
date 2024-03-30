using ISSHAR.Application.DTOs.BookingDTOs;

namespace ISSHAR.Application.Services
{
    public interface IBookingService
    {
        Task<ICollection<BookingDisplayDTO>> GetAllAsync();
        Task<BookingDisplayDTO> GetByIdAsync(int id);
        Task AddAsync(BookingDTO bookingDto);
        Task<bool> UpdateAsync(int id, BookingDTO bookingDto);
        Task<bool> DeleteAsync(int id);
    }
}
