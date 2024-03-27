
namespace ISSHAR.DAL.Entities
{
    public class Booking
    {
        public int BookingId { get; set; }
        public int HallId { get; set; }
        public int UserId { set; get; }
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set;}
        public User User { get; set; }
        public Hall Hall { get; set; }

    }
}
