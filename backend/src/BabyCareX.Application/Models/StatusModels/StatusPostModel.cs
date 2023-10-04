using System.ComponentModel.DataAnnotations;

namespace BabyCareX.Application.Models.StatusModels
{
    public class StatusPostModel
    {
        [Required(ErrorMessage = "The description field is required"), MinLength(4)]
        public string? Description { get; set; }
    }
}