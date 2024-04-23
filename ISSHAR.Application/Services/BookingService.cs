using AutoMapper;
using ISSHAR.Application.DTOs.BookingDTOs;
using ISSHAR.DAL.Entities;
using ISSHAR.DAL.Repositories;
using Microsoft.Extensions.Logging;

namespace ISSHAR.Application.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<IBookingService> _logger;

        public BookingService(IBookingRepository bookingRepository, IMapper mapper, ILogger<IBookingService> logger)
        {
            _bookingRepository = bookingRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ICollection<BookingDisplayDTO>> GetAllAsync()
        {
            try
            {
                var bookings = await _bookingRepository.GetAllAsync();
                var bookingDTOs = _mapper.Map<ICollection<BookingDisplayDTO>>(bookings);
                return bookingDTOs;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting all bookings.");
                throw;
            }
        }

        public async Task<BookingDisplayDTO> GetByIdAsync(int id)
        {
            try
            {
                var booking = await _bookingRepository.GetByIdAsync(id);
                var bookingDTO = _mapper.Map<BookingDisplayDTO>(booking);
                return bookingDTO;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting booking by ID: {BookingId}", id);
                throw;
            }
        }

        public async Task<bool> AddAsync(BookingDTO bookingDTO)
        {
            try
            {
                var booking = _mapper.Map<Booking>(bookingDTO);
                return await _bookingRepository.AddAsync(booking);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding booking.");
                throw;
            }
        }

        public async Task<bool> UpdateAsync(int id, BookingDTO bookingDTO)
        {
            try
            {
                var existingBooking = await _bookingRepository.GetByIdAsync(id);
                if (existingBooking is null)
                {
                    return false;
                }
                _mapper.Map(bookingDTO, existingBooking);
                await _bookingRepository.UpdateAsync(existingBooking);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating booking with ID: {BookingId}", id);
                throw;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var existingBooking = await _bookingRepository.GetByIdAsync(id);
                if (existingBooking is null)
                {
                    return false;
                }
                await _bookingRepository.DeleteAsync(existingBooking);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting booking with ID: {BookingId}", id);
                throw;
            }
        }
        public async Task<ICollection<BookingDisplayDTO>> GetByHallIdAsync(int hallId)
        {
            try
            {
                var bookings = await _bookingRepository.GetByHallIdAsync(hallId);
                var bookingDTOs = _mapper.Map<ICollection<BookingDisplayDTO>>(bookings);
                return bookingDTOs;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting bookings.");
                throw;
            }
        }
        public async Task<ICollection<BookingDisplayDTO>> GetByUserIdAsync(int userId)
        {
            try
            {
                var bookings = await _bookingRepository.GetByUserIdAsync(userId);
                var bookingDTOs = _mapper.Map<ICollection<BookingDisplayDTO>>(bookings);
                return bookingDTOs;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting bookings.");
                throw;
            }
        }
        public async Task<bool> CheckAvailabilityAsync(int hallId, DateTime startDate, DateTime endDate)
        {
            try
            {
                var overlappingBookings = await _bookingRepository.GetOverlappingBookingsAsync(hallId, startDate, endDate);
                return overlappingBookings.Count == 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while checking availability for hall ID: {HallId}", hallId);
                throw;
            }
        }


    }
}
