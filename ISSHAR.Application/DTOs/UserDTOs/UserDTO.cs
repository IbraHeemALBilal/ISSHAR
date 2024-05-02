using Microsoft.AspNetCore.Http;

namespace ISSHAR.Application.DTOs.UserDTOs
{
    public class UserDTO
    {
        public string FirstName { get; set; }
        public string FatherName { get; set; }
        public string GrandFatherName { get; set; }
        public string FamilyName { get; set; }
        public string Password { set; get; }
        public string Email { set; get; }
        public DateTime DateOfBirth { get; set; }
        public string City { set; get; }
        public string Gender { set; get; }
        public string Role { get; set; }
        public IFormFile? ImageFile { get; set; }

    }
}
