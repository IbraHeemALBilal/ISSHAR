﻿using ISSHAR.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace ISSHAR.DAL.Repositories
{
    public class CardRepository : ICardRepository
    {
        private readonly AppDbContext _context;
        public CardRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Card>> GetByCreaterIdAsync(int id)
        {
            return await _context.Cards.AsNoTracking().Where(c=>c.UserId==id)
                .OrderByDescending(c=>c.PartyDate).ToListAsync();
        }

        public async Task<Card> GetByIdAsync(int id)
        {
            return await _context.Cards.AsNoTracking().FirstOrDefaultAsync(a => a.CardId == id);
        }

        public async Task AddAsync(Card card)
        {
            await _context.Cards.AddAsync(card);
            await SaveChangesAsync();
        }

        public async Task DeleteAsync(Card card)
        {
             _context.Cards.Remove(card);
            await SaveChangesAsync();
        }

        private async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
