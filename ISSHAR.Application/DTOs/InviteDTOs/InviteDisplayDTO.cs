using ISSHAR.DAL.Entities;

namespace ISSHAR.Application.DTOs.InviteDTOs
{
    public class InviteDisplayDTO
    {
        public int InviteId { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public int CardId { get; set; }

        public User Sender { get; set; }
        public Card Card { get; set; }
    }
}
