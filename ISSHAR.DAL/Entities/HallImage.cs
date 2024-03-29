namespace ISSHAR.DAL.Entities
{
    public class HallImage
    {
        public int HallImageId { get; set; }
        public int HallId { get; set; }
        public string ImageUrl { get; set; }

        public Hall Hall { get; set; }

    }
}
