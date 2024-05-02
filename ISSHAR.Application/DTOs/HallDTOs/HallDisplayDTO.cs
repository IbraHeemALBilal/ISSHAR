using ISSHAR.Application.DTOs.HallImageDTOs;

namespace ISSHAR.Application.DTOs.HallDTOs
{
    public class HallDisplayDTO
    {
        public int HallId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public int Capacity { get; set; }
        public string Logo { get; set; }
        public int OwnerId { set; get; }
        public decimal PartyPrice { set; get; }

        public ICollection<HallImageDisplayDTO> HallImages { set; get; }
        public string Status { get; set; }
    }
}
