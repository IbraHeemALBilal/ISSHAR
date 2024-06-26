using ISSHAR.DAL.Entities;
using ISSHAR.DAL.Enums;
using ISSHAR.DAL.Extentions;
using Microsoft.EntityFrameworkCore;

namespace ISSHAR.DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ICollection<User>> GetAllAsync(int page, int pageSize)
        {
            return await _context.Users.AsNoTracking()
                .Where(u => u.Role != Role.Admin)
                .OrderBy(u => u.UserId) 
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }


        public async Task<User> GetByIdAsync(int id)
        {
            return await _context.Users.AsNoTracking().FirstOrDefaultAsync(u=>u.UserId== id);
        }

        public async Task AddAsync(User user)
        {
            user.HashPassword();
            await _context.Users.AddAsync(user);
            await SaveChangesAsync();
        }
        public async Task<ICollection<User>> GetReceiversOfCartAsync(int cartId)
        {
            return await _context.Users.AsNoTracking().Where(u=>u.ReceivedInvites.Any(r=>r.CardId== cartId)).ToListAsync();
        }
        public async Task<bool> CheckPasswordAsync(string email, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            return user?.VerifyPassword(password) ?? false;
        }
        private async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _context.Users.AsNoTracking().FirstOrDefaultAsync(e => e.Email == email);
        }
        public async Task<ICollection<User>> GetFilteredUsersAsync(
            string? firstName, string? fatherName,
            string? grandFatherName, string? familyName,
            string? city, string? gender)
        {
            return await _context.Users
                .Where(u => (string.IsNullOrWhiteSpace(firstName) || u.FirstName == firstName) &&
                            (string.IsNullOrWhiteSpace(fatherName) || u.FatherName == fatherName) &&
                            (string.IsNullOrWhiteSpace(grandFatherName) || u.GrandFatherName == grandFatherName) &&
                            (string.IsNullOrWhiteSpace(familyName) || u.FamilyName == familyName) &&
                            (string.IsNullOrWhiteSpace(city) || u.City == city) &&
                            (string.IsNullOrWhiteSpace(gender) || u.Gender == gender) &&
                            u.Role != Role.Admin)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
