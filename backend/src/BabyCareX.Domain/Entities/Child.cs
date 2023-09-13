using BabyCareX.Domain.Enums;

namespace BabyCareX.Domain.Entities
{
    public class Child : BaseEntity
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public Sex Sex { get; set; }
        public bool IsSpecialChild { get; set; }
        public int FamilyId { get; set; }
        public Family Family { get; set; }
    }
}