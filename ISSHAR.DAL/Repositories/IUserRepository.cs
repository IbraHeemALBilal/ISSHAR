using ISSHAR.DAL.Entities;

namespace ISSHAR.DAL.Repositories
{
    public interface IUserRepository
    {
        Task<ICollection<User>> GetAllAsync();
        Task<User> GetByIdAsync(int id);
        Task AddAsync(User user);
        Task<ICollection<User>> GetReceiversOfCartAsync(int cartId);
        Task <User> GetUserByEmailAsync(string email);
        Task<bool> CheckPasswordAsync(string email, string password);
        Task<ICollection<User>> GetFilteredUsersAsync(
           string? firstName, string? fatherName,
           string? grandFatherName, string? familyName,
           string? city, string? gender);
        Task SaveChangesAsync();

    }
}
