using ISSHAR.Application.DTOs.AdvertisementDTOs;
using ISSHAR.DAL.Enums;

namespace ISSHAR.Application.Services
{
    public interface IAdvertisementService
    {
        Task<ICollection<AdvertisementDisplayDTO>> GetAdsByStatusAsync(Status status);
        Task<AdvertisementDisplayDTO> GetByIdAsync(int id);
        Task<AdvertisementDisplayDTO> AddAsync(AdvertisementDTO advertisementDTO);
        Task<bool> DeleteAsync(int id);
        Task<ICollection<AdvertisementDisplayDTO>> GetAdsByUserAsync(int userId);
        Task<ICollection<AdvertisementDisplayDTO>> GetFilteredAdsAsync(AdvertisementFilterBody advertisementFilterBody);
        Task<bool> ChangeStatusAsync(int id, string newStatus);
    }
}
