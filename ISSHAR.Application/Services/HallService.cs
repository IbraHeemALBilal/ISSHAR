using AutoMapper;
using ISSHAR.Application.DTOs.HallDTOs;
using ISSHAR.DAL.Entities;
using ISSHAR.DAL.Enums;
using ISSHAR.DAL.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace ISSHAR.Application.Services
{
    public class HallService : IHallService
    {
        private readonly IHallRepository _hallRepository;
        private readonly IBookingRepository _bookingRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<IHallService> _logger;
        private readonly IImageService _cloudinary;

        public HallService(IHallRepository hallRepository,IBookingRepository bookingRepository, IMapper mapper, ILogger<IHallService> logger,IImageService cloudinary)
        {
            _hallRepository = hallRepository;
            _bookingRepository = bookingRepository;
            _mapper = mapper;
            _logger = logger;
            _cloudinary = cloudinary;
        }

        public async Task<ICollection<HallDisplayDTO>> GetHallsByStatusAsync(Status status, int page, int pageSize)
        {
            try
            {
                var halls = await _hallRepository.GetByStatusAsync(status, page, pageSize);
                var hallDtos = _mapper.Map<ICollection<HallDisplayDTO>>(halls);
                return hallDtos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting all halls.");
                throw;
            }
        }

        public async Task<HallDisplayDTO> GetByIdAsync(int id)
        {
            try
            {
                var hall = await _hallRepository.GetByIdAsync(id);
                var hallDto = _mapper.Map<HallDisplayDTO>(hall);
                return hallDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting hall by ID: {HallId}", id);
                throw;
            }
        }

        public async Task<HallDisplayDTO> AddAsync(HallDTO hallDTO)
        {
            try
            {
                var hall = _mapper.Map<Hall>(hallDTO);
                hall.HallImages = await UploadHallImagesAsync(hallDTO.ImagesAsFiles.ToList());
                hall.Logo = await GetHallLogoUrl(hallDTO.LogoFile);
                hall.Status = Status.Pending;
                await _hallRepository.AddAsync(hall);

                var hallDisplayDTO = _mapper.Map<HallDisplayDTO>(hall);
                return hallDisplayDTO;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding hall.");
                throw;
            }
        }

        public async Task<bool> UpdateAsync(int id, HallDTO hallDTO)
        {
            try
            {
                var existingHall = await _hallRepository.GetByIdAsync(id);
                if (existingHall is null)
                {
                    return false;
                }
                _mapper.Map(hallDTO, existingHall);
                await _hallRepository.UpdateAsync(existingHall);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating hall with ID: {HallId}", id);
                throw;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var existingHall = await _hallRepository.GetByIdAsync(id);
                if (existingHall is null)
                {
                    return false;
                }
                var hasFutureBookings = await _bookingRepository.HasFutureBookingsAsync(id);
                if(hasFutureBookings)
                {
                    return false;
                }
                var hallBookings = await _bookingRepository.GetByHallIdAsync(id);
                foreach(var booking in hallBookings)
                {
                    await _bookingRepository.DeleteAsync(booking);
                }
                await _hallRepository.DeleteAsync(existingHall);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting hall with ID: {HallId}", id);
                throw;
            }
        }

        public async Task<ICollection<HallDisplayDTO>> GetByOwnerIdAsync(int ownerId)
        {
            try
            {
                var halls = await _hallRepository.GetByOwnerIdAsync(ownerId);
                var hallDTOs = _mapper.Map<ICollection<HallDisplayDTO>>(halls);
                return hallDTOs;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting halls by owner with ID: {OwnerId}", ownerId);
                throw;
            }
        }

        public async Task<ICollection<HallDisplayDTO>> GetFilteredHallsAsync(HallFitlerBody filter)
        {
            try
            {
                var halls = await _hallRepository.GetFilteredHallsAsync(filter.City, filter.MinPrice, filter.MaxPrice);
                var hallDTOs = _mapper.Map<ICollection<HallDisplayDTO>>(halls);
                return hallDTOs;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting filtered halls.");
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
                var advertisement = await _hallRepository.GetByIdAsync(id);
                if (advertisement == null)
                {
                    return false;
                }
                advertisement.Status = status;
                await _hallRepository.UpdateAsync(advertisement);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while changing status of advertisement with ID: {AdvertisementId}", id);
                throw;
            }
        }

        private async Task<List<HallImage>> UploadHallImagesAsync(List<IFormFile> imagesAsFiles)
        {
            var hallImages = new List<HallImage>();
            foreach (var file in imagesAsFiles)
            {
                hallImages.Add(new HallImage { ImageUrl = await _cloudinary.UploadImageAsync(file) });
            }
            return hallImages;
        }

        private async Task<string> GetHallLogoUrl(IFormFile logoFile)
        {
            return logoFile == null ? DefaultImageUrls.HallLogoUrl : await _cloudinary.UploadImageAsync(logoFile);
        }
    }
}
