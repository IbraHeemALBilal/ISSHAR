﻿using ISSHAR.Application.DTOs.CardDTOs;
using ISSHAR.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ISSHAR.API.Controllers
{
    [Route("api/cards")]
    [ApiController]
    [Authorize(Roles = "Reguler, HallOwner")]
    public class CardController : ControllerBase
    { 
        private readonly ICardService _cardService;

        public CardController(ICardService cardService)
        {
            _cardService = cardService;
        }

        [HttpGet("creator/{id}")]
        [UserIdAuthorization("id")]
        public async Task<ActionResult<IEnumerable<CardDTO>>> GetCardsByCreatorId(int id)
        {
            var cards = await _cardService.GetByCreaterId(id);
            return Ok(cards);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<CardDTO>> GetCardById(int id)
        {
            var card = await _cardService.GetByIdAsync(id);
            if (card == null)
            {
                return NotFound();
            }
            return Ok(card);
        }

        [HttpPost]
        public async Task<IActionResult> AddCard(CardDTO cardDTO)
        {
            var card = await _cardService.AddAsync(cardDTO);
            return Ok(card);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCard(int id)
        {
            var result = await _cardService.DeleteAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
