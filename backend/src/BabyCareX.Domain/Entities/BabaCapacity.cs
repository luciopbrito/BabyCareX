namespace BabyCareX.Domain.Entities
{
    public class BabaCapacity : BaseEntity
    {
        public string Name { get; set; }
        public int BabaId { get; set; }
        public Baba Baba { get; set; }
    }
}