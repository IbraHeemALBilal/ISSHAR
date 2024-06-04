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

        public async Task<ICollection<User>> GetAllAsync()
        {
            return await _context.Users.AsNoTracking().Where(u=>u.Role != Role.Admin).ToListAsync();
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
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _context.Users.AsNoTracking().FirstOrDefaultAsync(e => e.Email == email);
        }
        public async Task<ICollection<User>> GetFilteredUsersAsync
            (string? firstName, string? fatherName,
            string? grandFatherName, string? familyName,
            string? city, string? gender)
        {
            var filteredUsers = _context.Users.AsQueryable();

            if (firstName != null)
                filteredUsers = filteredUsers.Where(u => u.FirstName == firstName);

            if (fatherName != null)
                filteredUsers = filteredUsers.Where(u => u.FatherName == fatherName);

            if (grandFatherName != null)
                filteredUsers = filteredUsers.Where(u => u.GrandFatherName == grandFatherName);

            if (familyName != null)
                filteredUsers = filteredUsers.Where(u => u.FamilyName == familyName);

            if (city != null)
                filteredUsers = filteredUsers.Where(u => u.City == city);

            if (gender != null)
                filteredUsers = filteredUsers.Where(u => u.Gender == gender);

            return await filteredUsers.AsNoTracking().Where(u=>u.Role!=Role.Admin).ToListAsync();
        }
    }
}
