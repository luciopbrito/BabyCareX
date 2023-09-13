namespace BabyCareX.Domain.Entities
{
    public class KindNanny : BaseEntity
    {
        public string Description { get; set; }
        public IEnumerable<BabaProvideService> BabaProvideServices { get; set; }
    }
}