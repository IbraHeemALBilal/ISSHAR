﻿using ISSHAR.DAL.Entities;
using ISSHAR.DAL.Enums;
using Microsoft.EntityFrameworkCore;

namespace ISSHAR.DAL.Repositories
{
    public class HallRepository : IHallRepository
    {
        private readonly AppDbContext _context;

        public HallRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<ICollection<Hall>> GetByStatusAsync(Status status, int page, int pageSize)
        {
            return await _context.Halls.AsNoTracking()
                .Where(h => h.Status == status)
                .OrderBy(h => h.HallId)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<Hall> GetByIdAsync(int id)
        {
            return await _context.Halls.AsNoTracking().Include(c => c.HallImages).FirstOrDefaultAsync(a => a.HallId == id);
        }
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
        public async Task UpdateAsync(Hall hall)
        {
            _context.Halls.Update(hall);
            await SaveChangesAsync();
        }
        public async Task<ICollection<Hall>> GetByOwnerIdAsync(int id)
        {
            return await _context.Halls.AsNoTracking().Where(a => a.OwnerId == id).ToListAsync();
        }
        private async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        public async Task<ICollection<Hall>> GetFilteredHallsAsync(string? city, decimal? minPrice, decimal? maxPrice)
        {
            return await _context.Halls.AsNoTracking()
                .Where(h =>
                    (city == null || h.City == city) &&
                    (!minPrice.HasValue || h.PartyPrice >= minPrice) &&
                    (!maxPrice.HasValue || h.PartyPrice <= maxPrice) &&
                    h.Status == Status.Approved
                )
                .ToListAsync();
        }
    }
}