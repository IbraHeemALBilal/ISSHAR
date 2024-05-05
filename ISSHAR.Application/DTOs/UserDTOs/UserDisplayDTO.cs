using ISSHAR.DAL.Enums;

namespace ISSHAR.Application.DTOs.UserDTOs
{
    public class UserDisplayDTO
    {
        public int UserId { set; get; }
        public string FirstName { get; set; }
        public string FatherName { get; set; }
        public string GrandFatherName { get; set; }
        public string FamilyName { get; set; }
        public string ImageUrl { get; set; }
        public string Password { set; get; }
        public string Email { set; get; }
        public Role Role { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string City { set; get; }
        public string Gender { set; get; }
    }
}
