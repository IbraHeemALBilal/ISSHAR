namespace ISSHAR.DAL.Entities
{
    public class Advertisement
    {
        public int AdvertisementId { get; set;}
        public int UserId { get; set;}
        public string Title { get; set;}
        public string Description { get; set;}
        public string ImageUrl { get; set;}
        public string PhoneNumber { get; set;}
        public string City { get; set;}
        public DateTime DatePosted { get; set;} = DateTime.Now;

        public string ServiceType { get; set;} 
        public User User { get; set; }
    }
}