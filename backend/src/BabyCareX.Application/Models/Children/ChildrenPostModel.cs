using System.ComponentModel.DataAnnotations;
using BabyCareX.Domain.Enums;

namespace BabyCareX.Application.Models.Children
{
    public class ChildrenPostModel
    {
        [Required(ErrorMessage = "The name field is required"), MinLength(3)]
        public string? Name { get; set; }
        [Required(ErrorMessage = "The age field is required"), Range(1, int.MaxValue, ErrorMessage = "Age must be greater than 0")]
        public int? Age { get; set; }
        [Required(ErrorMessage = "The sex field is required"), Range(1, 3, ErrorMessage = "Sex must be 1 to man 2 to woman and 3 to preserve.")]
        public ESex? Sex { get; set; }
        [Required(ErrorMessage = "The isSpecialChild field is required")]
        public bool? IsSpecialChild { get; set; }
        [Required(ErrorMessage = "The familyId field is required")]
        public int? FamilyId { get; set; }
    }
}