namespace ISSHAR.Application.DTOs.BookingDTOs
{
    public class BookingDisplayDTO
    {
        public int BookingId { get; set; }
        public int UserId { set; get; }
        public int HallId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
