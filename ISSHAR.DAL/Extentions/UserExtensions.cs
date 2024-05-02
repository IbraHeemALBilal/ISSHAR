using ISSHAR.DAL.Entities;

namespace ISSHAR.DAL.Extentions
{
    public static class UserExtensions
    {
        public static void HashPassword(this User user)
        {
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
        }
    }
}