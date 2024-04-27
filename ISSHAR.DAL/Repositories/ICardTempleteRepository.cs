using ISSHAR.DAL.Entities;

namespace ISSHAR.DAL.Repositories
{
    public interface ICardTempleteRepository
    {
        Task<ICollection<CardTemplet>> GetAllAsync();// get and order by creation date
        Task<CardTemplet> GetByIdAsync(int id);
        Task AddAsync(CardTemplet cardTemplet);
        Task DeleteAsync(CardTemplet cardTemplet);
        Task SaveChangesAsync();
    }
}
