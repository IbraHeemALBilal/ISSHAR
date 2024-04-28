using AutoMapper;
using ISSHAR.Application.DTOs.CardTempletDTOs;
using ISSHAR.DAL.Entities;
using ISSHAR.DAL.Repositories;
using Microsoft.Extensions.Logging;

namespace ISSHAR.Application.Services
{
    public class CardTempleteService : ICardTempleteService
    {
        private readonly ICardTempleteRepository _bookingRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<ICardTempleteService> _logger;

        public CardTempleteService(ICardTempleteRepository bookingRepository, IMapper mapper, ILogger<ICardTempleteService> logger)
        {
            _bookingRepository = bookingRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ICollection<CardTempletDisplayDTO>> GetAllAsync()
        {
            try
            {
                var cardTemplets = await _bookingRepository.GetAllAsync();
                var cardTempletsDTOs = _mapper.Map<ICollection<CardTempletDisplayDTO>>(cardTemplets);
                return cardTempletsDTOs;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting all CardTemplets.");
                throw;
            }
        }

        public async Task<CardTempletDisplayDTO> GetByIdAsync(int id)
        {
            try
            {
                var cardTemplet = await _bookingRepository.GetByIdAsync(id);
                var cardTempletDTO = _mapper.Map<CardTempletDisplayDTO>(cardTemplet);
                return cardTempletDTO;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting booking by ID: {BookingId}", id);
                throw;
            }
        }

        public async Task AddAsync(CardTempletDTO cardTempletDTO)
        {
            try
            {
                var cardTemplet = _mapper.Map<CardTemplet>(cardTempletDTO);
                await _bookingRepository.AddAsync(cardTemplet);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding a new CardTemplet.");
                throw;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var cardTemplet = await _bookingRepository.GetByIdAsync(id);
                if (cardTemplet == null)
                {
                    return false;
                }
                await _bookingRepository.DeleteAsync(cardTemplet);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting CardTemplet with ID: {CardTempletId}", id);
                throw;
            }
        }

    }
}
