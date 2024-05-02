using ISSHAR.Application.DTOs.UserDTOs;

namespace ISSHAR.Application.Services
{
    public interface IUserService
    {
        Task<ICollection<UserInfoDTO>> GetAllUsersAsync();
        Task<UserDisplayDTO> GetUserByIdAsync(int id);
        Task AddUserAsync(UserDTO userDTO);
        Task<ICollection<UserInfoDTO>> GetReceiversOfCartAsync(int cartId);
        Task<bool> CheckPasswordAsync(LoginBodyRequest loginBody);
    }
}
