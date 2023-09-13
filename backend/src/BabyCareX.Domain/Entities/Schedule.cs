namespace BabyCareX.Domain.Entities
{
    public class Schedule : BaseEntity
    {
        public int FamilyId { get; set; }
        public Family Family { get; set; }
        public int BabasId { get; set; }
        public Baba Baba { get; set; }
        public int TimesAWeek { get; set; }
    }
}