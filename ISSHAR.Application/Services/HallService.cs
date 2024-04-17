using AutoMapper;
using ISSHAR.Application.DTOs.HallDTOs;
using ISSHAR.DAL.Entities;
using ISSHAR.DAL.Repositories;
using Microsoft.Extensions.Logging;

namespace ISSHAR.Application.Services
{
    public class HallService : IHallService
    {
        private readonly IHallRepository _hallRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<IHallService> _logger;

        public HallService(IHallRepository hallRepository, IMapper mapper, ILogger<IHallService> logger)
        {
            _hallRepository = hallRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ICollection<HallDisplayDTO>> GetAllAsync()
        {
            try
            {
                var halls = await _hallRepository.GetAllAsync();
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

        public async Task AddAsync(HallDTO hallDTO)
        {
            try
            {
                var hall = _mapper.Map<Hall>(hallDTO);
                await _hallRepository.AddAsync(hall);
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


    }
}
