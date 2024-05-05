using ISSHAR.Application.DTOs.UserDTOs;
using ISSHAR.DAL.Entities;

namespace ISSHAR.Application.Services
{
    public interface IUserService
    {
        Task<ICollection<UserInfoDTO>> GetAllUsersAsync();
        Task<UserDisplayDTO> GetUserByIdAsync(int id);
        Task AddUserAsync(UserDTO userDTO);
        Task<ICollection<UserInfoDTO>> GetReceiversOfCartAsync(int cartId);
        Task<bool> CheckPasswordAsync(LoginBodyRequest loginBody);
        Task<UserDisplayDTO> GetUserByEmailAsync(string email);

    }
}
