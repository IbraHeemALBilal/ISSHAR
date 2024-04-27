namespace ISSHAR.DAL.Entities
{
    public class User
    {
        public int UserId { set; get;}
        public string FirstName { get; set; }
        public string FatherName { get; set; }
        public string GrandFatherName { get; set; }
        public string FamilyName { get; set; }
        public string ImageUrl { get; set; }
        public string Password { set; get;}
        public string Email { set; get;}
        public DateTime DateOfBirth { get; set;}
        public string City { set; get; }
        public string Gender { set; get; }
        public string Role { get; set;}

        public ICollection<Advertisement> Advertisements { set; get;}
        public ICollection<Hall> Halls { set; get; }
        public ICollection<Booking> Bookings { set; get; }
        public ICollection<Card> Cards { get; set; }
        public ICollection<Invite> SendedInvites{ get; set; }
        public ICollection<Invite> ReceivedInvites { get; set; }
    }
}
