using BabyCareX.Domain.Enums;

namespace BabyCareX.Domain.Entities
{
    public class Child : BaseEntity
    {
        public required string Name { get; set; }
        public required int Age { get; set; }
        public required ESex Sex { get; set; }
        public required bool IsSpecialChild { get; set; }
        public required int FamilyId { get; set; }
        public Family? Family { get; set; }
    }
}