using AutoMapper;
using ISSHAR.Application.DTOs.HallDTOs;
using ISSHAR.DAL.Entities;
using ISSHAR.DAL.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        public async Task AddAsync(HallDTO hallDto)
        {
            try
            {
                var hall = _mapper.Map<Hall>(hallDto);
                await _hallRepository.AddAsync(hall);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding hall.");
                throw;
            }
        }

        public async Task<bool> UpdateAsync(int id, HallDTO hallDto)
        {
            try
            {
                var existingHall = await _hallRepository.GetByIdAsync(id);
                if (existingHall == null)
                {
                    return false;
                }
                _mapper.Map(hallDto, existingHall);
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
                if (existingHall == null)
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
                var hallDtos = _mapper.Map<ICollection<HallDisplayDTO>>(halls);
                return hallDtos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting halls by owner with ID: {OwnerId}", ownerId);
                throw;
            }
        }
    }
}
