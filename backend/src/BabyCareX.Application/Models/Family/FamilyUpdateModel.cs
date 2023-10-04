using System.ComponentModel.DataAnnotations;
using BabyCareX.Domain.Entities;
using BabyCareX.Domain.Enums;

namespace BabyCareX.Application.Models.Family
{
    public class FamilyUpdateModel
    {
        [Required(ErrorMessage = "The familyName field is required"), MinLength(4)]
        public string? FamilyName { get; set; }
        public string? FatherName { get; set; }
        public string? MotherName { get; set; }
        [Required(ErrorMessage = "The phoneNumber field is required"), MinLength(9)]
        public string? PhoneNumber { get; set; }
        [Required(ErrorMessage = "The email field is required"), EmailAddress]
        public string? Email { get; set; }
        [Required(ErrorMessage = "The password field is required")]
        public string? Password { get; set; }
        public IEnumerable<Child>? Children { get; set; }
        public IEnumerable<Schedule>? Schedules { get; set; }
        [Required(ErrorMessage = "The createdAt field is required")]
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        [Required(ErrorMessage = "The statusId field is required"), Range(1, 4, ErrorMessage = "Status must be between 1 and 4")]
        public required EStatus? StatusId { get; set; }
    }
}