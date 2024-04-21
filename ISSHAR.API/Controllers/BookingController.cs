using ISSHAR.Application.DTOs.BookingDTOs;
using ISSHAR.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace ISSHAR.API.Controllers
{
    [Route("api/bookings")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;
        
        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<BookingDisplayDTO>>> GetAllAsync()
        {
            var bookings = await _bookingService.GetAllAsync();
            return Ok(bookings);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BookingDisplayDTO>> GetByIdAsync(int id)
        {
            var booking = await _bookingService.GetByIdAsync(id);
            if (booking is null)
                return NotFound();
            return Ok(booking);
        }

        [HttpPost]
        public async Task<ActionResult> AddAsync(BookingDTO bookingDTO)
        {
            await _bookingService.AddAsync(bookingDTO);
            return Ok("Booking added successfully.");
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAsync(int id, BookingDTO bookingDTO)
        {
            var success = await _bookingService.UpdateAsync(id, bookingDTO);
            if (!success)
                return NotFound("Booking not found.");

            return Ok("Booking updated successfully.");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var success = await _bookingService.DeleteAsync(id);
            if (!success)
                return NotFound("Booking not found.");

            return Ok("Booking deleted successfully.");
        }

        [HttpGet("hall/{hallId}")]
        public async Task<ActionResult<ICollection<BookingDisplayDTO>>> GetByHallIdAsync(int hallId)
        {
            var bookings = await _bookingService.GetByHallIdAsync(hallId);
            return Ok(bookings);
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<ICollection<BookingDisplayDTO>>> GetByUserIdAsync(int userId)
        {
            var bookings = await _bookingService.GetByUserIdAsync(userId);
            return Ok(bookings);
        }
        [HttpPost("hall/{hallId}/availability")]
        public async Task<ActionResult<bool>> CheckHallAvailabilityAsync(int hallId, [FromBody] DateRangeDTO dateRange)
        {
            var isAvailable = await _bookingService.CheckAvailabilityAsync(hallId, dateRange.StartDate, dateRange.EndDate);
            return Ok(isAvailable);
        }

    }
}
