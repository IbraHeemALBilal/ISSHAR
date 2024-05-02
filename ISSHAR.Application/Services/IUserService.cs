﻿using ISSHAR.Application.DTOs.UserDTOs;

namespace ISSHAR.Application.Services
{
    public interface IUserService
    {
        Task<ICollection<UserDisplayDTO>> GetAllUsersAsync();
        Task<UserDisplayDTO> GetUserByIdAsync(int id);
        Task AddUserAsync(UserDTO userDTO);
        Task<ICollection<UserInfoDTO>> GetReceiversOfCartAsync(int cartId);
    }
}
