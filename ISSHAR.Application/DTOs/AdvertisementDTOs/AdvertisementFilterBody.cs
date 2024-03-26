using System.ComponentModel.DataAnnotations;

namespace ISSHAR.Application.DTOs.AdvertisementDTOs
{
    public class AdvertisementFilterBody
    {
        [StringLength(25, ErrorMessage = "City must be at most 25 characters long")]
        public string? City { get; set; }

        [StringLength(25, ErrorMessage = "ServiceType must be at most 25 characters long")]
        public string? ServiceType { get; set; }
    }
}
