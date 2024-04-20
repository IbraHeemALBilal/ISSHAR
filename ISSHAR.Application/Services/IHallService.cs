using ISSHAR.Application.DTOs.HallDTOs;

namespace ISSHAR.Application.Services
{
    public interface IHallService
    {
        Task<ICollection<HallDisplayDTO>> GetAllAsync();
        Task<HallDisplayDTO> GetByIdAsync(int id);
        Task<HallDisplayDTO> AddAsync(HallDTO hallDTO);
        Task<bool> UpdateAsync(int id, HallDTO hallDTO);
        Task<bool> DeleteAsync(int id);
        Task<ICollection<HallDisplayDTO>> GetByOwnerIdAsync(int id);
        Task<ICollection<HallDisplayDTO>> GetFilteredHallsAsync(HallFitlerBody filter);

    }
}
