namespace BabyCareX.Domain.Entities
{
    public class Baba : BaseEntity
    {
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string PhoneNumber { get; set; }
        public required string Password { get; set; }
        public string? Description { get; set; }
        public IEnumerable<Schedule>? Schedules { get; set; }
        public IEnumerable<BabaProvideService>? BabaProvideServices { get; set; }
        public IEnumerable<BabaCourse>? BabaCourses { get; set; }
        public IEnumerable<BabaCapacity>? BabaCapacities { get; set; }
    }
}