using ISSHAR.Application.DTOs.HallDTOs;
using ISSHAR.DAL.Enums;

namespace ISSHAR.Application.Services
{
    public interface IHallService
    {
        Task<ICollection<HallDisplayDTO>> GetHallsByStatusAsync(Status status, int page, int pageSize);
        Task<HallDisplayDTO> GetByIdAsync(int id);
        Task<HallDisplayDTO> AddAsync(HallDTO hallDTO);
        Task<bool> UpdateAsync(int id, HallDTO hallDTO);
        Task<bool> DeleteAsync(int id);
        Task<ICollection<HallDisplayDTO>> GetByOwnerIdAsync(int id);
        Task<ICollection<HallDisplayDTO>> GetFilteredHallsAsync(HallFitlerBody filter);
        Task<bool> ChangeStatusAsync(int id, string newStatus);
    }
}
