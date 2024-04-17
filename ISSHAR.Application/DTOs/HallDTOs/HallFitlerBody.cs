using System.ComponentModel.DataAnnotations;

namespace ISSHAR.Application.DTOs.HallDTOs
{
    public class HallFitlerBody
    {
        [MaxLength(100)]
        public string? City { get; set; }

        [Range(0, double.MaxValue)]
        public decimal? MinPrice { get; set; }

        [Range(0, double.MaxValue)]
        public decimal? MaxPrice { get; set; }
    }
}
