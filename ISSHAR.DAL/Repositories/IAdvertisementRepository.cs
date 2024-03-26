using ISSHAR.DAL.Entities;

namespace ISSHAR.DAL.Repositories
{
    public interface IAdvertisementRepository
    {
        Task<ICollection<Advertisement>> GetAllAsync();
        Task<Advertisement> GetByIdAsync(int id);
        Task AddAsync(Advertisement advertisement);
        Task UpdateAsync(Advertisement advertisement);
        Task DeleteAsync(Advertisement advertisement);
        Task SaveChangesAsync();
        Task<ICollection<Advertisement>> GetAdsByUserAsync(int userId);
        Task<ICollection<Advertisement>> GetFilteredAdsAsync(string? city, string? serviceType);
    }
}
