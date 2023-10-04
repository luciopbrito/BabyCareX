using System.ComponentModel.DataAnnotations;
using BabyCareX.Domain.Enums;

namespace BabyCareX.Application.Models.Children
{
    public class ChildrenUpdateModel
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
        [Required(ErrorMessage = "The createdAt field is required")]
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        [Required(ErrorMessage = "The statusId field is required"), Range(1, 4, ErrorMessage = "Status must be between 1 and 4")]
        public EStatus? StatusId { get; set; }
    }
}