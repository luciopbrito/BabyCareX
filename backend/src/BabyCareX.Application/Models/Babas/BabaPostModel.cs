using System.ComponentModel.DataAnnotations;
using BabyCareX.Domain.Entities;

namespace BabyCareX.Application.Models.Babas
{
    public class BabaPostModel
    {
        [Required(ErrorMessage = "The name field is required"), MinLength(3)]
        public string? Name { get; set; }
        [Required(ErrorMessage = "The email field is required"), EmailAddress]
        public string? Email { get; set; }
        [Required(ErrorMessage = "The password field is required")]
        public string? Password { get; set; }
        [Required(ErrorMessage = "The phoneNumber field is required"), MinLength(9)]
        public string? PhoneNumber { get; set; }
        public string? Description { get; set; }
        public IEnumerable<Schedule>? Schedules { get; set; }
        public IEnumerable<BabaProvideService>? BabaProvideServices { get; set; }
        public IEnumerable<BabaCourse>? BabaCourses { get; set; }
        public IEnumerable<BabaCapacity>? BabaCapacities { get; set; }
    }
}