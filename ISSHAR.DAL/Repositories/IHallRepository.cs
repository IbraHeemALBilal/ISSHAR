using ISSHAR.DAL.Entities;

namespace ISSHAR.DAL.Repositories
{
    public interface IHallRepository
    {
        Task<ICollection<Hall>> GetAllAsync();
        Task<Hall> GetByIdAsync(int id);
        Task AddAsync(Hall hall);
        Task UpdateAsync(Hall hall);
        Task DeleteAsync(Hall hall);
        Task SaveChangesAsync();
        Task<ICollection<Hall>> GetByOwnerIdAsync(int id);
        Task<ICollection<Hall>> GetFilteredHallsAsync(string? city, decimal? minPrice, decimal? maxPrice);

    }
}
