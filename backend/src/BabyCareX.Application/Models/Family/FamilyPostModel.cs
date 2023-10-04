using System.ComponentModel.DataAnnotations;
using BabyCareX.Domain.Entities;

namespace BabyCareX.Application.Models.Family
{
    public class FamilyPostModel
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
    }
}