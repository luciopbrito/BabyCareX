using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using BabyCareX.Domain.Entities;
using BabyCareX.Domain.Enums;

namespace BabyCareX.Application.Models.Babas
{
    public class BabaUpdateModel
    {
        [Required(ErrorMessage = "The name field is required"), MinLength(3)]
        public string? Name { get; set; }
        [Required(ErrorMessage = "The email field is required"), EmailAddress]
        public string? Email { get; set; }
        [Required(ErrorMessage = "The password field is required"), MinLength(4)]
        public string? Password { get; set; }
        [Required(ErrorMessage = "The phoneNumber field is required"), MinLength(9)]
        public string? PhoneNumber { get; set; }
        public string? Description { get; set; }
        public IEnumerable<Schedule>? Schedules { get; set; }
        public IEnumerable<BabaProvideService>? BabaProvideServices { get; set; }
        public IEnumerable<BabaCourse>? BabaCourses { get; set; }
        public IEnumerable<BabaCapacity>? BabaCapacities { get; set; }
        public DateTime? UpdatedAt { get; set; }
        [Required(ErrorMessage = "The createAt field is required")]
        public DateTime? CreatedAt { get; set; }
        [Required(ErrorMessage = "The statusId field is required"), Range(1, 4, ErrorMessage = "Status must be between 1 and 4")]
        public EStatus? StatusId { get; set; }
    }
}