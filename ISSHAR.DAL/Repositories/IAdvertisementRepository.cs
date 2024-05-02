using ISSHAR.DAL.Entities;
using ISSHAR.DAL.Enums;

namespace ISSHAR.DAL.Repositories
{
    public interface IAdvertisementRepository
    {
        Task<ICollection<Advertisement>> GetByStatusAsync(Status status);
        Task<Advertisement> GetByIdAsync(int id);
        Task AddAsync(Advertisement advertisement);
        Task UpdateAsync(Advertisement advertisement);
        Task DeleteAsync(Advertisement advertisement);
        Task SaveChangesAsync();
        Task<ICollection<Advertisement>> GetAdsByUserAsync(int userId);
        Task<ICollection<Advertisement>> GetFilteredAdsAsync(string? city, string? serviceType);
    }
}
