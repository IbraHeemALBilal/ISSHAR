using ISSHAR.Application.DTOs.CardTempletDTOs;

namespace ISSHAR.Application.Services
{
    public interface ICardTempleteService
    {
        Task<ICollection<CardTempletDisplayDTO>> GetAllAsync();
        Task<CardTempletDisplayDTO> GetByIdAsync(int id);
        Task AddAsync(CardTempletDTO cardTempletDTO);
        Task<bool> DeleteAsync(int id);
    }
}
