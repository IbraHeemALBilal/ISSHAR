using ISSHAR.DAL.Entities;
using ISSHAR.DAL.Enums;
using Microsoft.EntityFrameworkCore;

namespace ISSHAR.DAL.Repositories
{
    public class AdvertisementRepository: IAdvertisementRepository
    {
        private readonly AppDbContext _context;
        public AdvertisementRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<ICollection<Advertisement>> GetByStatusAsync(Status status, int page, int pageSize)
        {
            return await _context.Advertisements.AsNoTracking()
                .Where(a => a.Status == status)
                .OrderByDescending(a => a.DatePosted)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
        public async Task<Advertisement> GetByIdAsync(int id)
        {
            return await _context.Advertisements.AsNoTracking().FirstOrDefaultAsync(a => a.AdvertisementId == id);
        }
        public async Task AddAsync(Advertisement advertisement)
        {
            await _context.Advertisements.AddAsync(advertisement);
            await SaveChangesAsync();
        }
        public async Task UpdateAsync(Advertisement advertisement)
        {
            _context.Advertisements.Update(advertisement);
            await SaveChangesAsync();
        }
        public async Task DeleteAsync(Advertisement advertisement)
        {
            _context.Advertisements.Remove(advertisement);
            await SaveChangesAsync();
        }
        private async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        public async Task<ICollection<Advertisement>> GetAdsByUserAsync(int userId)
        {
            return await _context.Advertisements.AsNoTracking()
                .Where(a=>a.UserId== userId)
                .OrderBy(a=>a.Status)
                .ThenByDescending(a=>a.DatePosted)
                .ToListAsync();
        }
        public async Task<ICollection<Advertisement>> GetFilteredAdsAsync(string? city, string? serviceType)
        {
            var filteredAds = await _context.Advertisements.AsNoTracking()
                .Where(ad => (city == null || ad.City == city) &&
                    (serviceType == null || ad.ServiceType == serviceType)&&
                    ad.Status== Status.Approved)
                .OrderByDescending(a=>a.DatePosted)
                .ToListAsync();

            return filteredAds;
        }
    }
}
