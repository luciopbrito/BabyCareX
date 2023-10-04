using System.ComponentModel.DataAnnotations;

namespace BabyCareX.Application.Models.StatusModels
{
    public class StatusUpdateModel
    {
        [Required(ErrorMessage = "The description field is required"), MinLength(4)]
        public string? Description { get; set; }
        [Required(ErrorMessage = "The createdAt field is required")]
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}