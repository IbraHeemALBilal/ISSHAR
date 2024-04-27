namespace ISSHAR.DAL.Entities
{
    public class Card
    {
        public int CardId { get; set; }
        public string Title { get; set; }
        public DateTime PartyDate { get; set; }
        public string JsonData { get; set; }
        public int UserId { get; set;}

        public User User { get; set; }
        public ICollection<Invite> Invites { set; get; }

    }
}
