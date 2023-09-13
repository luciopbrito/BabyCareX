namespace BabyCareX.Domain.Entities
{
    public class BabaProvideService : BaseEntity
    {
        public string Name { get; set; }
        public int KindNannyId { get; set; }
        public KindNanny KindNanny { get; set; }
        public int BabaId { get; set; }
        public Baba Baba { get; set; }
    }
}