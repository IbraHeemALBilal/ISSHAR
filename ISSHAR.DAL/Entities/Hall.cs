
namespace ISSHAR.DAL.Entities
{
    public class Hall
    {
        public int HallId { get; set; }
        public int UserId { set; get; }
        public int Capacity { get; set; }
        public string Name { get; set; }
        public string location { get; set;}
        public User User { get; set; }
        public ICollection<HallImage> HallImages { set; get; }
        public ICollection<Booking> Bookings { set; get; }


    }
}
