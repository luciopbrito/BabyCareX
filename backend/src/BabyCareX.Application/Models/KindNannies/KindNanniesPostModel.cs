using System.ComponentModel.DataAnnotations;

namespace BabyCareX.Application.Models.KindNannies
{
    public class KindNanniesPostModel
    {
        [Required(ErrorMessage = "The name field is required"), MinLength(3)]
        public string? Name { get; set; }
        [Required(ErrorMessage = "The description field is required"), MinLength(4)]
        public string? Description { get; set; }
    }
}