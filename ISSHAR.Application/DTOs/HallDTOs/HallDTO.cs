using ISSHAR.Application.DTOs.HallImageDTOs;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ISSHAR.Application.DTOs.HallDTOs
{
    public class HallDTO
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }

        [Required(ErrorMessage = "City is required")]
        public string City { get; set; }

        [Required(ErrorMessage = "Capacity is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Capacity must be a positive number")]
        public int Capacity { get; set; }
        public IFormFile LogoFile { get; set; }

        [Required(ErrorMessage = "Owner Id is required")]
        public int OwnerId { get; set; }

        [Required(ErrorMessage = "Party Price is required")]
        [Range(0, double.MaxValue, ErrorMessage = "Party Price must be a positive number")]
        public decimal PartyPrice { get; set; }

        [MinLength(2, ErrorMessage = "At least 2 images are required.")]
        [MaxLength(5, ErrorMessage = "At most 5 images are allowed.")]
        public ICollection<IFormFile> ImagesAsFiles { get; set; }
    }
}
