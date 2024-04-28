

using ISSHAR.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace ISSHAR.DAL.Repositories
{
    public class InviteRepository : IInviteRepository
    {
        private readonly AppDbContext _context;
        public InviteRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Invite invite)
        {
            await _context.invites.AddAsync(invite);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> CheckIfInvitedBeforAsnyc(int senderId, int receiverId, int cardId)
        {
            return await _context.invites
          .AnyAsync(i => i.SenderId == senderId && i.ReceiverId == receiverId && i.CardId == cardId);
        }

        public async Task<Invite> GetByIdAsync(int id)
        {
            return await _context.invites.AsNoTracking().Include(c => c.Sender).FirstOrDefaultAsync(a => a.InviteId == id);
        }

        public async Task<ICollection<Hall>> GetByReceiverIdAsync(int receiverId)// There may be problems
        {
            return (ICollection<Hall>)await _context.invites
        .AsNoTracking()
        .Where(i => i.ReceiverId == receiverId)
        .Include(c => c.Sender)
        .ToListAsync();

        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
