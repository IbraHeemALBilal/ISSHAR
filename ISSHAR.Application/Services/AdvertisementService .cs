using AutoMapper;
using ISSHAR.Application.DTOs.AdvertisementDTOs;
using ISSHAR.Application.Survices;
using ISSHAR.DAL.Entities;
using ISSHAR.DAL.Repositories;
using Microsoft.Extensions.Logging;

namespace ISSHAR.Application.Services
{
    public class AdvertisementService : IAdvertisementService
    {
        private readonly IAdvertisementRepository _advertisementRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<IAdvertisementService> _logger;

        public AdvertisementService(IAdvertisementRepository advertisementRepository, IMapper mapper, ILogger<IAdvertisementService> logger)
        {
            _advertisementRepository = advertisementRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ICollection<AdvertisementDisplayDTO>> GetAllAsync()
        {
            try
            {
                var advertisements = await _advertisementRepository.GetAllAsync();
                var advertisementDTOs = _mapper.Map<ICollection<AdvertisementDisplayDTO>>(advertisements);
                return advertisementDTOs;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting all advertisements.");
                throw;
            }
        }

        public async Task<AdvertisementDisplayDTO> GetByIdAsync(int id)
        {
            try
            {
                var advertisement = await _advertisementRepository.GetByIdAsync(id);
                var advertisementDTO = _mapper.Map<AdvertisementDisplayDTO>(advertisement);
                return advertisementDTO;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting advertisement by ID: {AdvertisementId}", id);
                throw;
            }
        }

        public async Task AddAsync(AdvertisementDTO advertisementDTO)
        {
            try
            {
                var advertisement = _mapper.Map<Advertisement>(advertisementDTO);
                await _advertisementRepository.AddAsync(advertisement);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding advertisement.");
                throw;
            }
        }

        public async Task<bool> UpdateAsync(int id, AdvertisementDTO advertisementDTO)
        {
            try
            {
                var existingAdvertisement = await _advertisementRepository.GetByIdAsync(id);
                if (existingAdvertisement is null)
                {
                    return false;
                }
                _mapper.Map(advertisementDTO, existingAdvertisement);
                await _advertisementRepository.UpdateAsync(existingAdvertisement);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating advertisement with ID: {AdvertisementId}", id);
                throw;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var existingAdvertisement = await _advertisementRepository.GetByIdAsync(id);
                if (existingAdvertisement is null)
                {
                    return false;
                }
                await _advertisementRepository.DeleteAsync(existingAdvertisement);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting advertisement with ID: {AdvertisementId}", id);
                throw;
            }
        }

        public async Task<ICollection<AdvertisementDisplayDTO>> GetAdsByUserAsync(int userId)
        {
            try
            {
                var advertisements = await _advertisementRepository.GetAdsByUserAsync(userId);
                var advertisementDTOs = _mapper.Map<ICollection<AdvertisementDisplayDTO>>(advertisements);
                return advertisementDTOs;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting advertisements by user with ID: {UserId}", userId);
                throw;
            }
        }

        public async Task<ICollection<AdvertisementDisplayDTO>> GetFilteredAdsAsync(AdvertisementFilterBody advertisementFilterBody)
        {
            try
            {
                var advertisements = await _advertisementRepository.GetFilteredAdsAsync(advertisementFilterBody.City, advertisementFilterBody.ServiceType);
                var advertisementDTOs = _mapper.Map<ICollection<AdvertisementDisplayDTO>>(advertisements);
                return advertisementDTOs;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting filtered advertisements.");
                throw;
            }
        }


    }
}
