using BabyCareX.Domain.Enums;

namespace BabyCareX.Domain.Entities
{
    public class Family : BaseEntity
    {
        public required string FamilyName { get; set; }
        public string? FatherName { get; set; }
        public string? MotherName { get; set; }
        public required string PhoneNumber { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public IEnumerable<Child>? Children { get; set; }
        public IEnumerable<Schedule>? Schedules { get; set; }
    }
}