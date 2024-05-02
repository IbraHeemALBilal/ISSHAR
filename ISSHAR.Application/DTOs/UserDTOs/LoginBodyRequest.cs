using System.ComponentModel.DataAnnotations;

namespace ISSHAR.Application.DTOs.UserDTOs
{
    public class LoginBodyRequest
    {
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}
