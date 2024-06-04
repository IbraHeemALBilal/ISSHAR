using ISSHAR.Application.DTOs.HallDTOs;
using ISSHAR.Application.Services;
using ISSHAR.DAL.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ISSHAR.API.Controllers
{
    [Route("api/halls")]
    [ApiController]
    public class HallController : ControllerBase
    {
        private readonly IHallService _hallService;
        public HallController(IHallService hallService)
        {
            _hallService = hallService;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<HallDisplayDTO>>> GetAllAsync()
        {
            var halls = await _hallService.GetHallsByStatusAsync(Status.Approved);
            return Ok(halls);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("pending")]
        public async Task<ActionResult<ICollection<HallDisplayDTO>>> GetPendingAsync()
        {
            var halls = await _hallService.GetHallsByStatusAsync(Status.Pending);
            return Ok(halls);
        }
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<HallDisplayDTO>> GetByIdAsync(int id)
        {
            var hall = await _hallService.GetByIdAsync(id);
            if (hall == null)
                return NotFound();
            return Ok(hall);
        }
        [Authorize(Roles = "HallOwner")]
        [HttpPost]
        public async Task<ActionResult> AddAsync([FromForm] HallDTO hallDTO)
        {
            var hall = await _hallService.AddAsync(hallDTO);
            return Ok(hall);
        }
        [Authorize(Roles = "HallOwner")]
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAsync(int id, HallDTO hallDTO)
        {
            var success = await _hallService.UpdateAsync(id, hallDTO);
            if (!success)
                return NotFound("Hall not found.");

            return Ok("Hall updated successfully.");
        }
        [Authorize(Roles = "HallOwner")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var success = await _hallService.DeleteAsync(id);
            if (!success)
                return NotFound("Hall not found or have future bookings.");
            return Ok("Hall deleted successfully.");
        }
        [Authorize(Roles = "HallOwner")]
        [HttpGet("owner/{ownerId}")]
        public async Task<ActionResult<ICollection<HallDisplayDTO>>> GetByOwnerId(int ownerId)
        {
            var halls = await _hallService.GetByOwnerIdAsync(ownerId);
            return Ok(halls);
        }
        [HttpGet("filtered")]
        public async Task<ActionResult<ICollection<HallDisplayDTO>>> GetFilteredAsync([FromQuery] HallFitlerBody hallFitlerBody)
        {
            var halls = await _hallService.GetFilteredHallsAsync(hallFitlerBody);
            return Ok(halls);
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}/status")]
        public async Task<ActionResult> ChangeStatusAsync(int id, [FromQuery] string newStatus)
        {
            var success = await _hallService.ChangeStatusAsync(id, newStatus);
            if (!success)
                return NotFound("Hall not found or new status not allowed.");

            return Ok("Hall status changed successfully.");
        }
    }
}
