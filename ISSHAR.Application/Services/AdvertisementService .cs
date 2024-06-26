using AutoMapper;
using ISSHAR.Application.DTOs.AdvertisementDTOs;
using ISSHAR.Application.Services;
using ISSHAR.DAL.Entities;
using ISSHAR.DAL.Enums;
using ISSHAR.DAL.Repositories;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace ISSHAR.Application.Services
{
    public class AdvertisementService : IAdvertisementService
    {
        private readonly IAdvertisementRepository _advertisementRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<IAdvertisementService> _logger;
        private readonly IImageService _cloudinary;
        public AdvertisementService(IAdvertisementRepository advertisementRepository, IMapper mapper, ILogger<IAdvertisementService> logger, IImageService cloudinary)
        {
            _advertisementRepository = advertisementRepository;
            _mapper = mapper;
            _logger = logger;
            _cloudinary = cloudinary;
        }

        public async Task<ICollection<AdvertisementDisplayDTO>> GetAdsByStatusAsync(Status status, int page, int pageSize)
        {
            try
            {
                var advertisements = await _advertisementRepository.GetByStatusAsync(status, page, pageSize);
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

        public async Task<AdvertisementDisplayDTO> AddAsync(AdvertisementDTO advertisementDTO)
        {
            try
            {
                var advertisement = _mapper.Map<Advertisement>(advertisementDTO);
                advertisement.ImageUrl = await GetAdvertisementImageUrl(advertisementDTO);
                advertisement.Status = Status.Pending;
                await _advertisementRepository.AddAsync(advertisement);

                var advertisementDisplayDTO = _mapper.Map<AdvertisementDisplayDTO>(advertisement);
                return advertisementDisplayDTO;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding advertisement.");
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
        public async Task<bool> ChangeStatusAsync(int id, string newStatus)
        {
            try
            {
                if (!Enum.TryParse(newStatus, out Status status))
                {
                    return false;
                }
                var advertisement = await _advertisementRepository.GetByIdAsync(id);
                if (advertisement == null)
                {
                    return false;
                }
                advertisement.Status = status;
                await _advertisementRepository.UpdateAsync(advertisement);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while changing status of advertisement with ID: {AdvertisementId}", id);
                throw;
            }
        }

        private async Task<string> GetAdvertisementImageUrl(AdvertisementDTO advertisementDTO)
        {
            return advertisementDTO.ImageFile == null
                ? DefaultImageUrls.AdvertisementImageUrl
                : await _cloudinary.UploadImageAsync(advertisementDTO.ImageFile);
        }


    }
}
