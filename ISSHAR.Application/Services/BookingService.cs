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
                var bookingDtos = _mapper.Map<ICollection<BookingDisplayDTO>>(bookings);
                return bookingDtos;
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
                var bookingDto = _mapper.Map<BookingDisplayDTO>(booking);
                return bookingDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting booking by ID: {BookingId}", id);
                throw;
            }
        }

        public async Task AddAsync(BookingDTO bookingDto)
        {
            try
            {
                var booking = _mapper.Map<Booking>(bookingDto);
                await _bookingRepository.AddAsync(booking);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding booking.");
                throw;
            }
        }

        public async Task<bool> UpdateAsync(int id, BookingDTO bookingDto)
        {
            try
            {
                var existingBooking = await _bookingRepository.GetByIdAsync(id);
                if (existingBooking == null)
                {
                    return false;
                }
                _mapper.Map(bookingDto, existingBooking);
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
                if (existingBooking == null)
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
    }
}
