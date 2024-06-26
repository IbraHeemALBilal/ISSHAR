using ISSHAR.DAL.Entities;
using ISSHAR.DAL.Enums;

namespace ISSHAR.DAL.Repositories
{
    public interface IHallRepository
    {
        Task<ICollection<Hall>> GetByStatusAsync(Status status, int page, int pageSize);
        Task<Hall> GetByIdAsync(int id);
        Task AddAsync(Hall hall);
        Task UpdateAsync(Hall hall);
        Task DeleteAsync(Hall hall);
        Task<ICollection<Hall>> GetByOwnerIdAsync(int id);
        Task<ICollection<Hall>> GetFilteredHallsAsync(string? city, decimal? minPrice, decimal? maxPrice);
    }
}