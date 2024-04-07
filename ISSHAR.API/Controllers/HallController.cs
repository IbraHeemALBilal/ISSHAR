using ISSHAR.Application.DTOs.HallDTOs;
using ISSHAR.Application.Services;
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
            var halls = await _hallService.GetAllAsync();
            return Ok(halls);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<HallDisplayDTO>> GetByIdAsync(int id)
        {
            var hall = await _hallService.GetByIdAsync(id);
            if (hall == null)
                return NotFound();
            return Ok(hall);
        }

        [HttpPost]
        public async Task<ActionResult> AddAsync(HallDTO hallDTO)
        {
            await _hallService.AddAsync(hallDTO);
            return Ok("Hall added successfully.");
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAsync(int id, HallDTO hallDTO)
        {
            var success = await _hallService.UpdateAsync(id, hallDTO);
            if (!success)
                return NotFound("Hall not found.");

            return Ok("Hall updated successfully.");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var success = await _hallService.DeleteAsync(id);
            if (!success)
                return NotFound("Hall not found.");

            return Ok("Hall deleted successfully.");
        }

        [HttpGet("{ownerId}")]
        public async Task<ActionResult<ICollection<HallDisplayDTO>>> GetByOwnerId(int ownerId)
        {
            var halls = await _hallService.GetByOwnerIdAsync(ownerId);
            return Ok(halls);
        }

    }
}
