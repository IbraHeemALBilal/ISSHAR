using ISSHAR.Application.DTOs.UserDTOs;
using ISSHAR.DAL.Entities;

namespace ISSHAR.Application.Services
{
    public interface IUserService
    {
        Task<ICollection<UserDisplayDTO>> GetAllUsersAsync();
        Task<UserDisplayDTO> GetUserByIdAsync(int id);
        Task AddUserAsync(UserDTO userDTO);
    }
}
