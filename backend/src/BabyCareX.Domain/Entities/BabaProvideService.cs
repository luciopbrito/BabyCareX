namespace BabyCareX.Domain.Entities
{
    public class BabaProvideService : BaseEntity
    {
        public required int KindNannyId { get; set; }
        public KindNanny? KindNanny { get; set; }
        public required int BabaId { get; set; }
        public Baba? Baba { get; set; }
    }
}