using AutoMapper;
using ISSHAR.Application.DTOs.HallDTOs;
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
        private readonly IImageService _cloudinary;
        public UserService(IUserRepository userRepository, IMapper mapper , ILogger<IUserService> logger, IImageService cloudinary)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _logger = logger;
            _cloudinary = cloudinary;
        }

        public async Task<ICollection<UserInfoDTO>> GetAllUsersAsync()
        {
            try
            {
                var users = await _userRepository.GetAllAsync();
                var userDTOs = _mapper.Map<ICollection<UserInfoDTO>>(users);
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
                user.ImageUrl =await GetUserImageUrl(userDTO);
                await _userRepository.AddAsync(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating user.");
                throw;
            }
        }

        public async Task<ICollection<UserInfoDTO>> GetReceiversOfCartAsync(int cartId)
        {
            try
            {
                var users = await _userRepository.GetReceiversOfCartAsync(cartId);
                var userDTOs = _mapper.Map<ICollection<UserInfoDTO>>(users);
                return userDTOs;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting receivers Of cart.");
                throw;
            }
        }
        public async Task<UserDisplayDTO> GetUserByEmailAsync(string email)
        {
            try
            {
                var user = await _userRepository.GetUserByEmailAsync(email);
                var userDTO = _mapper.Map<UserDisplayDTO>(user);
                return userDTO;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting User by id.");
                throw;
            }
        }
        public async Task<bool> CheckPasswordAsync(LoginBodyRequest loginBody)
        {
            try
            {
                return await _userRepository.CheckPasswordAsync(loginBody.Email, loginBody.Password);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while checking user's password.");
                throw;
            }
        }

        private async Task<string> GetUserImageUrl(UserDTO userDTO)
        {
            string defaultImageUrl = userDTO.Gender == "Male" ? DefaultImageUrls.MaleImageUrl : DefaultImageUrls.FemaleImageUrl;

            return userDTO.ImageFile == null ? defaultImageUrl : await _cloudinary.UploadImageAsync(userDTO.ImageFile);
        }

        public async Task<ICollection<UserInfoDTO>> GetFilteredHallsAsync(UserFilterBody filter)
        {
            try
            {
                var users = await _userRepository.GetFilteredUsersAsync(filter.FirstName,filter.FatherName,filter.GrandFatherName,
                                                                        filter.FamilyName,filter.City,filter.Gender);
                var UserDTOs = _mapper.Map<ICollection<UserInfoDTO>>(users);
                return UserDTOs;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting filtered Users.");
                throw;
            }
        }
    }
}
