using ISSHAR.Application.DTOs.AdvertisementDTOs;
using ISSHAR.Application.Services;
using ISSHAR.DAL.Enums;
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
        public async Task<ActionResult<ICollection<AdvertisementDisplayDTO>>> GetAllAsync()
        {
            var advertisements = await _advertisementService.GetAdsByStatusAsync(Status.Approved);
            return Ok(advertisements);
        }
        [HttpGet("pending")]
        public async Task<ActionResult<ICollection<AdvertisementDisplayDTO>>> GetPendingAsync()
        {
            var advertisements = await _advertisementService.GetAdsByStatusAsync(Status.Pending);
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
        [HttpPost]
        public async Task<ActionResult<AdvertisementDisplayDTO>> AddAsync([FromForm] AdvertisementDTO advertisementDTO)
        {
            var advertisement = await _advertisementService.AddAsync(advertisementDTO);

            return Ok(advertisement);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var success = await _advertisementService.DeleteAsync(id);
            if (!success)
                return NotFound("Advertisement not found.");

            return Ok("Advertisement deleted successfully.");
        }

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
