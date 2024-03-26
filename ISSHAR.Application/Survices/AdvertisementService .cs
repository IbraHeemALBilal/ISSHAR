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
        private readonly ILogger<AdvertisementService> _logger;

        public AdvertisementService(IAdvertisementRepository advertisementRepository, IMapper mapper, ILogger<AdvertisementService> logger)
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
                var advertisementDtos = _mapper.Map<ICollection<AdvertisementDisplayDTO>>(advertisements);
                return advertisementDtos;
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
                var advertisementDto = _mapper.Map<AdvertisementDisplayDTO>(advertisement);
                return advertisementDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting advertisement by ID: {AdvertisementId}", id);
                throw;
            }
        }

        public async Task AddAsync(AdvertisementDTO advertisementDto)
        {
            try
            {
                var advertisement = _mapper.Map<Advertisement>(advertisementDto);
                await _advertisementRepository.AddAsync(advertisement);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding advertisement.");
                throw;
            }
        }

        public async Task<bool> UpdateAsync(int id, AdvertisementDTO advertisementDto)
        {
            try
            {
                var existingAdvertisement = await _advertisementRepository.GetByIdAsync(id);
                if (existingAdvertisement == null)
                {
                    return false;
                }
                _mapper.Map(advertisementDto, existingAdvertisement);
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
                if (existingAdvertisement == null)
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
                var advertisementDtos = _mapper.Map<ICollection<AdvertisementDisplayDTO>>(advertisements);
                return advertisementDtos;
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
                var advertisementDtos = _mapper.Map<ICollection<AdvertisementDisplayDTO>>(advertisements);
                return advertisementDtos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting filtered advertisements.");
                throw;
            }
        }
    }
}
