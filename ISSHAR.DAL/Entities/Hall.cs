using ISSHAR.DAL.Enums;

namespace ISSHAR.DAL.Entities
{
    public class Hall
    {
        public int HallId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set;}
        public string City { get; set; }
        public int Capacity { get; set; }
        public string Logo { get; set; }
        public int OwnerId { set; get; }
        public decimal PartyPrice { set; get; }
        public User Owner { get; set; }
        public Status Status { get; set; }

        public ICollection<HallImage> HallImages { set; get; }
        public ICollection<Booking> Bookings { set; get; }
    }
}
