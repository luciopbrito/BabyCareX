namespace BabyCareX.Domain.Entities
{
    public class Baba : BaseEntity
    {
        public string Description { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public IEnumerable<Schedule> Schedules { get; set; }
        public IEnumerable<BabaProvideService> BabaProvideServices { get; set; }
        public IEnumerable<BabaCourse> BabaCourses { get; set; }
        public IEnumerable<BabaCapacity> BabaCapacities { get; set; }
    }
}