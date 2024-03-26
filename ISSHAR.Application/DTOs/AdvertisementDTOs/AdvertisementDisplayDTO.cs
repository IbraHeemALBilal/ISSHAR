namespace ISSHAR.Application.DTOs.AdvertisementDTOs
{
    public class AdvertisementDisplayDTO
    {
        public int AdvertisementId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string PhoneNumber { get; set; }
        public string City { get; set; }
        public DateTime DatePosted { get; set; }
        public string ServiceType { get; set; }
    }
}
