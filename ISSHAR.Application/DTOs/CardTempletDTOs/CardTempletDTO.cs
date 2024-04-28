using System.ComponentModel.DataAnnotations;

namespace ISSHAR.Application.DTOs.CardTempletDTOs
{
    public class CardTempletDTO
    {
        [Required(ErrorMessage = "JsonData is required")]
        public string JsonData { get; set; }
    }
}
