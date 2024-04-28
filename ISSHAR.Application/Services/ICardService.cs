using ISSHAR.Application.DTOs.CardDTOs;

namespace ISSHAR.Application.Services
{
    public interface ICardService
    {
        Task<ICollection<CardDisplayDTO>> GetAllAsync();
        Task<CardDisplayDTO> GetByIdAsync(int id);
        Task AddAsync(CardDTO cardDTO);
        Task<bool> DeleteAsync(int id);
    }
}
