namespace BabyCareX.Domain.Entities
{
    public class KindNanny : BaseEntity
    {
        public required string Description { get; set; }
        public required string Name { get; set; }
        public IEnumerable<BabaProvideService>? BabaProvideServices { get; set; }
    }
}