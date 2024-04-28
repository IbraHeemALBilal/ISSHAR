using ISSHAR.Application.DTOs.CardDTOs;
using ISSHAR.Application.DTOs.UserDTOs;

namespace ISSHAR.Application.DTOs.InviteDTOs
{
    public class InviteDisplayDTO
    {
        public int InviteId { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public int CardId { get; set; }

        public SenderDTO Sender { get; set; }
        public CardDisplayDTO Card { get; set; }
    }
}
