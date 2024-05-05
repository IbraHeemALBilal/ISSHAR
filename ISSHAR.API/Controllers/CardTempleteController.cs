using ISSHAR.Application.DTOs.CardTempletDTOs;
using ISSHAR.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace ISSHAR.API.Controllers
{
    [Route("api/cardtemplates")]
    [ApiController]
    [Authorize]
    public class CardTemplateController : ControllerBase
    {
        private readonly ICardTempleteService _cardTemplateService;

        public CardTemplateController(ICardTempleteService cardTemplateService)
        {
            _cardTemplateService = cardTemplateService;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<CardTempletDisplayDTO>>> GetAllCardTemplates()
        {
            var cardTemplates = await _cardTemplateService.GetAllAsync();
            return Ok(cardTemplates);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CardTempletDisplayDTO>> GetCardTemplateById(int id)
        {
            var cardTemplate = await _cardTemplateService.GetByIdAsync(id);
            if (cardTemplate == null)
            {
                return NotFound();
            }
            return Ok(cardTemplate);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AddCardTemplate(CardTempletDTO cardTemplateDTO)
        {
            await _cardTemplateService.AddAsync(cardTemplateDTO);
            return Ok();
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCardTemplate(int id)
        {
            var result = await _cardTemplateService.DeleteAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
