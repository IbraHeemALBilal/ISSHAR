using AutoMapper;
using ISSHAR.Application.DTOs.CardDTOs;
using ISSHAR.DAL.Entities;
using ISSHAR.DAL.Repositories;
using Microsoft.Extensions.Logging;

namespace ISSHAR.Application.Services
{
    public class CardService : ICardService
    {
        private readonly ICardRepository _cardRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<ICardService> _logger;

        public CardService(ICardRepository cardRepository, IMapper mapper, ILogger<ICardService> logger)
        {
            _cardRepository = cardRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ICollection<CardDisplayDTO>> GetByCreaterId(int id)
        {
            try
            {
                var cards = await _cardRepository.GetByCreaterIdAsync(id);
                var cardDTOs = _mapper.Map<ICollection<CardDisplayDTO>>(cards);
                return cardDTOs;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting all cards.");
                throw;
            }
        }

        public async Task<CardDisplayDTO> GetByIdAsync(int id)
        {
            try
            {
                var card = await _cardRepository.GetByIdAsync(id);
                var cardDTO = _mapper.Map<CardDisplayDTO>(card);
                return cardDTO;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting card by ID: {CardId}", id);
                throw;
            }
        }

        public async Task<CardDisplayDTO> AddAsync(CardDTO cardDTO)
        {
            try
            {
                var card = _mapper.Map<Card>(cardDTO);
                await _cardRepository.AddAsync(card);
                var cardDisplayDTO = _mapper.Map<CardDisplayDTO>(card);
                return cardDisplayDTO;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding a new card.");
                throw;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var card = await _cardRepository.GetByIdAsync(id);
                if (card == null)
                {
                    return false;
                }
                await _cardRepository.DeleteAsync(card);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting card with ID: {CardId}", id);
                throw;
            }
        }
    }
}
