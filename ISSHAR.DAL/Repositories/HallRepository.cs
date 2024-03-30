﻿using ISSHAR.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace ISSHAR.DAL.Repositories
{
    public class HallRepository : IHallRepository
    {
        private readonly AppDbContext _context;

        public async Task AddAsync(Hall hall)
        {
            await _context.Halls.AddAsync(hall);
            await SaveChangesAsync();
        }
        public async Task DeleteAsync(Hall hall)
        {
            _context.Halls.Remove(hall);
            await SaveChangesAsync();
        }
        public async Task<ICollection<Hall>> GetAllAsync()
        {
            return await _context.Halls.AsNoTracking().ToListAsync();

        }
        public async Task<Hall> GetByIdAsync(int id)
        {
            return await _context.Halls.AsNoTracking().Include(c => c.HallImages).FirstOrDefaultAsync(a => a.HallId == id);
        }

        public async Task<ICollection<Hall>> GetHallsByOwnerIdAsync(int id)
        {
            return await _context.Halls.AsNoTracking().Include(c => c.HallImages).Where(a => a.OwnerId == id).ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Hall hall)
        {
            _context.Halls.Update(hall);
            await SaveChangesAsync();
        }
    }
}