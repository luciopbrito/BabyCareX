namespace BabyCareX.Domain.Entities
{
    public class BabaCapacity : BaseEntity
    {
        public required string Name { get; set; }
        public required int BabaId { get; set; }
        public Baba? Baba { get; set; }
    }
}