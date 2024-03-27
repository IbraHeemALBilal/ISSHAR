namespace ISSHAR.DAL.Entities
{
    public class User
    {
        public int UserId { set; get;}
        public string FullName { set; get;}
        public string Password { set; get;}
        public string Email { set; get;}
        public DateTime DateOfBirth { get; set;}
        public string Role { get; set;}

        public ICollection<Advertisement> Advertisements { set; get;}
        public ICollection<Hall> Halls { set; get; }
        public ICollection<Booking> Bookings { set; get; }


    }
}
