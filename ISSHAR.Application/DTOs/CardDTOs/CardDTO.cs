using System.ComponentModel.DataAnnotations;

namespace ISSHAR.Application.DTOs.CardDTOs
{
    public class CardDTO
    {
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Party date is required")]
        public DateTime PartyDate { get; set; }
        [Required(ErrorMessage = "JsonData  is required")]
        public string JsonData { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "User ID must be a positive integer")]
        public int UserId { get; set; }
    }
}
