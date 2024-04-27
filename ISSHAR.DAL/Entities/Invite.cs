namespace ISSHAR.DAL.Entities
{
    public class Invite
    {
        public int InviteId { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public int CardId { get; set; }

        public User Sender { get; set; }
        public User Receiver { get; set; }
        public Card Card { get; set; }
    }
}
