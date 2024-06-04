namespace ISSHAR.Application.DTOs.UserDTOs
{
    public class UserFilterBody
    {
        public string? FirstName { get; set; }
        public string? FatherName { get; set; }
        public string? GrandFatherName { get; set; }
        public string? FamilyName { get; set; }
        public string? City { set; get; }
        public string? Gender { set; get; }
    }
}
