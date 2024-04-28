using ISSHAR.DAL.Entities;

namespace ISSHAR.DAL.Repositories
{
    public interface ICardRepository
    {
        Task<ICollection<Card>> GetByCreaterIdAsync(int id);
        Task<Card> GetByIdAsync(int id);
        Task AddAsync(Card card);
        Task DeleteAsync(Card card);
        Task SaveChangesAsync();
    }
}
