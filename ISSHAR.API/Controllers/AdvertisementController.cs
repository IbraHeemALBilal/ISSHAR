using ISSHAR.Application.DTOs.AdvertisementDTOs;
using ISSHAR.Application.Services;
using ISSHAR.DAL.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ISSHAR.API.Controllers
{
    [Route("api/advertisements")]
    [ApiController]
    public class AdvertisementController : ControllerBase
    {
        private readonly IAdvertisementService _advertisementService;

        public AdvertisementController(IAdvertisementService advertisementService)
        {
            _advertisementService = advertisementService;
        }
        [HttpGet]
        public async Task<ActionResult<ICollection<AdvertisementDisplayDTO>>> GetAllAsync(int page = 1, int pageSize = 10)
        {
            var advertisements = await _advertisementService.GetAdsByStatusAsync(Status.Approved, page, pageSize);
            return Ok(advertisements);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("pending")]
        public async Task<ActionResult<ICollection<AdvertisementDisplayDTO>>> GetPendingAsync(int page = 1, int pageSize = 10)
        {
            var advertisements = await _advertisementService.GetAdsByStatusAsync(Status.Pending, page, pageSize);
            return Ok(advertisements);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<AdvertisementDisplayDTO>> GetByIdAsync(int id)
        {
            var advertisement = await _advertisementService.GetByIdAsync(id);
            if (advertisement is null)
                return NotFound();
            return Ok(advertisement);
        }
        [Authorize(Roles = "Reguler, HallOwner")]
        [HttpPost]
        public async Task<ActionResult<AdvertisementDisplayDTO>> AddAsync([FromForm] AdvertisementDTO advertisementDTO)
        {
            var advertisement = await _advertisementService.AddAsync(advertisementDTO);

            return Ok(advertisement);
        }
        [Authorize(Roles = "Reguler, HallOwner")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var success = await _advertisementService.DeleteAsync(id);
            if (!success)
                return NotFound("Advertisement not found.");

            return Ok("Advertisement deleted successfully.");
        }
        [Authorize(Roles = "Reguler, HallOwner")]
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<ICollection<AdvertisementDisplayDTO>>> GetAdsByUserAsync(int userId)
        {
            var advertisements = await _advertisementService.GetAdsByUserAsync(userId);
            return Ok(advertisements);
        }

        [HttpGet("filtered")]
        public async Task<ActionResult<ICollection<AdvertisementDisplayDTO>>> GetFilteredAdsAsync([FromQuery] AdvertisementFilterBody filterBody)
        {
            var advertisements = await _advertisementService.GetFilteredAdsAsync(filterBody);
            return Ok(advertisements);
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}/status")]
        public async Task<ActionResult> ChangeStatusAsync(int id, [FromQuery] string newStatus)
        {
            var success = await _advertisementService.ChangeStatusAsync(id, newStatus);
            if (!success)
                return NotFound("Advertisement not found or new status not allowed.");

            return Ok("Advertisement status changed successfully.");
        }

    }
}
