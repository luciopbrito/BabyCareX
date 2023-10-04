namespace BabyCareX.Domain.Entities
{
    public class BabaCourse : BaseEntity
    {
        public required string Name { get; set; }
        public required DateTime StartPeriod { get; set; }
        public DateTime? EndPeriod { get; set; }
        public required bool IsCompleted { get; set; }
        public required int BabaId { get; set; }
        public Baba? Baba { get; set; }
    }
}