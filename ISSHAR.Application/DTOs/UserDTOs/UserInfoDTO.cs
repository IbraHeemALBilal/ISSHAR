namespace ISSHAR.Application.DTOs.UserDTOs
{
    public class UserInfoDTO
    {
        public int UserId { set; get; }
        public string FirstName { get; set; }
        public string FatherName { get; set; }
        public string GrandFatherName { get; set; }
        public string FamilyName { get; set; }
        public string City { set; get; }
        public string ImageUrl { get; set; }
    }
}