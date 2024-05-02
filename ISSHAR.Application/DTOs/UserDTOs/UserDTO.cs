using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace ISSHAR.Application.DTOs.UserDTOs
{
    public class UserDTO
    {
        [Required(ErrorMessage = "First name is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Father's name is required")]
        public string FatherName { get; set; }

        [Required(ErrorMessage = "Grandfather's name is required")]
        public string GrandFatherName { get; set; }

        [Required(ErrorMessage = "Family name is required")]
        public string FamilyName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Date of birth is required")]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "City is required")]
        public string City { get; set; }

        [Required(ErrorMessage = "Gender is required")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Role is required")]
        public string Role { get; set; }
        public IFormFile? ImageFile { get; set; }
    }
}