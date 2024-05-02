using ISSHAR.Application.Attributes;
using System.ComponentModel.DataAnnotations;

namespace ISSHAR.Application.DTOs.BookingDTOs
{
    public class BookingDTO
    {
        [Required(ErrorMessage = "UserId is required.")]
        public int UserId { set; get; }
        [Required(ErrorMessage = "HallId is required.")]
        public int HallId { get; set; }
        [Required(ErrorMessage = "Start date is required.")]
        [DataType(DataType.DateTime)]
        [LaterThanNow(ErrorMessage = "Start date must be later than current date.")]
        public DateTime StartDate { get; set; }
        [Required(ErrorMessage = "End date is required.")]
        [DataType(DataType.DateTime)]
        [LaterThanNow(ErrorMessage = "End date must be later than current date.")]
        public DateTime EndDate { get; set; }
    }
}
