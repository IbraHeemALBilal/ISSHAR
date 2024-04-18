using ISSHAR.Application.DTOs.AdvertisementDTOs;

namespace ISSHAR.Application.Services
{
    public interface IAdvertisementService
    {
        Task<ICollection<AdvertisementDisplayDTO>> GetAllAsync();
        Task<AdvertisementDisplayDTO> GetByIdAsync(int id);
        Task<AdvertisementDisplayDTO> AddAsync(AdvertisementDTO advertisementDTO);
        Task<bool> UpdateAsync(int id, AdvertisementDTO advertisementDTO);
        Task<bool> DeleteAsync(int id);
        Task<ICollection<AdvertisementDisplayDTO>> GetAdsByUserAsync(int userId);
        Task<ICollection<AdvertisementDisplayDTO>> GetFilteredAdsAsync(AdvertisementFilterBody advertisementFilterBody);
    }
}
