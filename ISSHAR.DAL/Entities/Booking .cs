namespace ISSHAR.DAL.Entities
{
    public class Booking
    {
        public int BookingId { get; set; }
        public int UserId { set; get; }
        public int HallId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set;}

        public User User { get; set; }
        public Hall Hall { get; set; }

    }
}
