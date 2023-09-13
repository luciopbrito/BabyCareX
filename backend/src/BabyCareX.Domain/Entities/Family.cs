namespace BabyCareX.Domain.Entities
{
    public class Family : BaseEntity
    {
        public string Name { get; set; }
        public string? FatherName { get; set; }
        public string? MotherName { get; set; }
        public string PhoneNumber { get; set; }
        public IEnumerable<Child> Children { get; set; }
        public IEnumerable<Schedule> Schedules { get; set; }
    }
}