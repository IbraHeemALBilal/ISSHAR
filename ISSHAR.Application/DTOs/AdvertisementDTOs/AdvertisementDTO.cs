using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ISSHAR.Application.DTOs.AdvertisementDTOs
{
    public class AdvertisementDTO
    {
        [Required(ErrorMessage = "UserId is required")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [StringLength(100, ErrorMessage = "Title must be at most 100 characters long")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Phone Number is required")]
        [RegularExpression(@"^05\d{8}$", ErrorMessage = "Invalid PhoneNumber format")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "City is required")]
        [StringLength(50, ErrorMessage = "City must be at most 50 characters long")]
        public string City { get; set; }

        [Required(ErrorMessage = "ServiceType is required")]
        [StringLength(50, ErrorMessage = "ServiceType must be at most 50 characters long")]
        public string ServiceType { get; set; }
        public IFormFile? ImageFile { get; set; }

    }
}
