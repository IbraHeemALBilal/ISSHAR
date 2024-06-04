using ISSHAR.Application.DTOs.BookingDTOs;
using ISSHAR.Application.Services;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<ICollection<BookingDisplayDTO>>> GetAllAsync()
        {
            var bookings = await _bookingService.GetAllAsync();
            return Ok(bookings);
        }
        [Authorize(Roles = "Reguler, HallOwner")]
        [HttpGet("{id}")]
        public async Task<ActionResult<BookingDisplayDTO>> GetByIdAsync(int id)
        {
            var booking = await _bookingService.GetByIdAsync(id);
            if (booking is null)
                return NotFound();
            return Ok(booking);
        }
        [Authorize(Roles = "Reguler, HallOwner")]
        [HttpPost]
        public async Task<ActionResult> AddAsync(BookingDTO bookingDTO)
        {
            var success = await _bookingService.AddAsync(bookingDTO);
            if (success)
                return Ok("Booking added successfully.");
            else return Conflict("Booking conflicts with existing booking.");
        }
        [Authorize(Roles = "Reguler, HallOwner")]
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAsync(int id, BookingDTO bookingDTO)
        {
            var success = await _bookingService.UpdateAsync(id, bookingDTO);
            if (!success)
                return NotFound("Booking not found.");

            return Ok("Booking updated successfully.");
        }
        [Authorize(Roles = "Reguler, HallOwner")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var success = await _bookingService.DeleteAsync(id);
            if (!success)
                return Conflict("Start date is within 7 days.");
            return Ok("Booking deleted successfully.");
        }
        [Authorize(Roles = "HallOwner")]
        [HttpGet("hall/{hallId}")]
        public async Task<ActionResult<ICollection<BookingDisplayDTO>>> GetByHallIdAsync(int hallId)
        {
            var bookings = await _bookingService.GetByHallIdAsync(hallId);
            return Ok(bookings);
        }
        [Authorize(Roles = "HallOwner")]
        [HttpGet("hall/{hallId}/{date}")]
        public async Task<ActionResult<ICollection<BookingDisplayDTO>>> GetByHallIdAndDateAsync(int hallId, string date)
        {
            if (!DateOnly.TryParse(date, out var parsedDate))
                return BadRequest("Invalid date format.");
            var bookings = await _bookingService.GetByHallIdAndDateAsync(hallId, parsedDate);
            return Ok(bookings);
        }

        [Authorize(Roles = "Reguler, HallOwner")]
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<ICollection<BookingDisplayDTO>>> GetByUserIdAsync(int userId)
        {
            var bookings = await _bookingService.GetByUserIdAsync(userId);
            return Ok(bookings);
        }
        [Authorize(Roles = "Reguler, HallOwner")]
        [HttpPost("hall/{hallId}/availability")]
        public async Task<ActionResult<bool>> CheckHallAvailabilityAsync(int hallId, [FromBody] DateRangeDTO dateRange)
        {
            var hasConflict = await _bookingService.CheckConflictAsync(hallId, dateRange.StartDate, dateRange.EndDate);
            bool result;
            if (hasConflict)
                result = false;
            else result = true;
            return Ok(result);
        }
    }
}
