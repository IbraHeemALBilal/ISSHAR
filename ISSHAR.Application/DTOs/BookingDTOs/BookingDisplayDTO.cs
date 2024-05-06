using ISSHAR.Application.DTOs.HallDTOs;
using ISSHAR.Application.DTOs.UserDTOs;

namespace ISSHAR.Application.DTOs.BookingDTOs
{
    public class BookingDisplayDTO
    {
        public int BookingId { get; set; }
        public int UserId { set; get; }
        public int HallId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public UserInfoDTO User { set; get; }
        public HallDisplayDTO Hall { get; set; }
    }
}
