using System.ComponentModel.DataAnnotations;

namespace ISSHAR.Application.DTOs.InviteDTOs
{
    public class InviteDTO
    {
        [Required(ErrorMessage = "SenderId is required")]
        public int SenderId { get; set; }
        [Required(ErrorMessage = "ReceiverId is required")]
        public int ReceiverId { get; set; }
        [Required(ErrorMessage = "CardId is required")]
        public int CardId { get; set; }
    }
}
