namespace ISSHAR.DAL.Entities
{
    public class CardTemplet
    {
        public int CardTempletId { get; set; }
        public string JsonData { get; set; }
        public DateTime CreatedAt { get; set; }= DateTime.Now;
    }
}
