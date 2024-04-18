namespace ISSHAR.Application.DTOs.UserDTOs
{
    public class UserDTO
    {
        public string FullName { set; get; }
        public string Password { set; get; }
        public string Email { set; get; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { set; get; }
        public string Role { get; set; }
    }
}
