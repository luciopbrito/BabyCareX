namespace BabyCareX.Domain.Entities
{
    public class BabaCourse
    {
        public string Name { get; set; }
        public bool IsCompleted { get; set; }
        public string StartPeriod { get; set; }
        public string EndPeriod { get; set; }
        public int BabaId { get; set; }
        public Baba Baba { get; set; }
    }
}