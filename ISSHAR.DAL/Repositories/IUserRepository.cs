using ISSHAR.DAL.Entities;

namespace ISSHAR.DAL.Repositories
{
    public interface IUserRepository
    {
        Task<ICollection<User>> GetAllAsync();
        Task<User> GetByIdAsync(int id);
        Task AddAsync(User user);
        Task<ICollection<User>> GetReceiversOfCartAsync(int cartId);
        Task<bool> CheckPasswordAsync(string email, string password);
        Task SaveChangesAsync();

    }
}
