using ISSHAR.Application.DTOs.CardDTOs;

namespace ISSHAR.Application.Services
{
    public interface ICardService
    {
        Task<ICollection<CardDisplayDTO>> GetByCreaterId(int id);
        Task<CardDisplayDTO> GetByIdAsync(int id);
        Task AddAsync(CardDTO cardDTO);
        Task<bool> DeleteAsync(int id);
    }
}
