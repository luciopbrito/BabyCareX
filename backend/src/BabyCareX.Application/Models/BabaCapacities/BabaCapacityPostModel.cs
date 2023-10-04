using System.ComponentModel.DataAnnotations;

namespace BabyCareX.Application.Models.BabaCapacities
{
    public class BabaCapacityPostModel
    {
        [Required(ErrorMessage = "The name is required"), MinLength(4)]
        public string? Name { get; set; }
        [Required(ErrorMessage = "The babaId is required")]
        public int? BabaId { get; set; }
    }
}