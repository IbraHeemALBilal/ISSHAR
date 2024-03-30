using ISSHAR.Application.DTOs.HallDTOs;

namespace ISSHAR.Application.Survices
{
    public interface IHallService
    {
        Task<ICollection<HallDisplayDTO>> GetAllAsync();
        Task<HallDisplayDTO> GetByIdAsync(int id);
        Task AddAsync(HallDTO hallDto);
        Task<bool> UpdateAsync(int id, HallDTO hallDto);
        Task<bool> DeleteAsync(int id);
        Task<ICollection<HallDisplayDTO>> GetByOwnerIdAsync();

    }
}
