using AutoMapper;
using ISSHAR.Application.DTOs.UserDTOs;
using ISSHAR.DAL.Entities;
using ISSHAR.DAL.Repositories;
using Microsoft.Extensions.Logging;

namespace ISSHAR.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<IUserService> _logger;

        public UserService(IUserRepository userRepository, IMapper mapper , ILogger<IUserService> logger)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ICollection<UserDisplayDTO>> GetAllUsersAsync()
        {
            try
            {
                var users = await _userRepository.GetAllAsync();
                var userDTOs = _mapper.Map<ICollection<UserDisplayDTO>>(users);
                return userDTOs;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting all Users.");
                throw;
            }
        }

        public async Task<UserDisplayDTO> GetUserByIdAsync(int id)
        {
            try
            {
                var user = await _userRepository.GetByIdAsync(id);
                var userDTO = _mapper.Map<UserDisplayDTO>(user);
                return userDTO;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting User by id.");
                throw;
            }
        }

        public async Task AddUserAsync(UserDTO userDTO)
        {
            try
            {
                var user = _mapper.Map<User>(userDTO);
                await _userRepository.AddAsync(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while Create User .");
                throw;
            }
        }
    }
}
