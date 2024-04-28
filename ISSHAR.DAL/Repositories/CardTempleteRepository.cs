using ISSHAR.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace ISSHAR.DAL.Repositories
{
    public class CardTempleteRepository : ICardTempleteRepository
    {
        private readonly AppDbContext _context;
        public CardTempleteRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ICollection<CardTemplet>> GetAllAsync()
        {
            return await _context.CardTemplets
                .OrderBy(c => c.CreatedAt)
                .ToListAsync();
        }

        public async Task<CardTemplet> GetByIdAsync(int id)
        {
            return await _context.CardTemplets.AsNoTracking().FirstOrDefaultAsync(a => a.CardTempletId == id);
        }
        public async Task AddAsync(CardTemplet cardTemplet)
        {
            await _context.CardTemplets.AddAsync(cardTemplet);
            await SaveChangesAsync();
        }

        public async Task DeleteAsync(CardTemplet cardTemplet)
        {
            _context.CardTemplets.Remove(cardTemplet);
            await SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
