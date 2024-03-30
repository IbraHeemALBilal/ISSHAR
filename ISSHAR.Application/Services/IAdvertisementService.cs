using ISSHAR.Application.DTOs.AdvertisementDTOs;

namespace ISSHAR.Application.Survices
{
    public interface IAdvertisementService
    {
        Task<ICollection<AdvertisementDisplayDTO>> GetAllAsync();
        Task<AdvertisementDisplayDTO> GetByIdAsync(int id);
        Task AddAsync(AdvertisementDTO advertisementDto);
        Task<bool> UpdateAsync(int id, AdvertisementDTO advertisementDto);
        Task<bool> DeleteAsync(int id);
        Task<ICollection<AdvertisementDisplayDTO>> GetAdsByUserAsync(int userId);
        Task<ICollection<AdvertisementDisplayDTO>> GetFilteredAdsAsync(AdvertisementFilterBody advertisementFilterBody);
    }
}
