using ISSHAR.DAL.Entities;

namespace ISSHAR.DAL.Extentions
{
    public static class UserExtensions
    {
        public static void HashPassword(this User user)
        {
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
        }
        public static bool VerifyPassword(this User user, string password)
        {
            if (user is null)
                return false;

            return BCrypt.Net.BCrypt.Verify(password, user.Password);
        }
    }
}