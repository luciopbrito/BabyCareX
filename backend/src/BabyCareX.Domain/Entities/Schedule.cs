using System.ComponentModel.DataAnnotations.Schema;
using BabyCareX.Domain.Enums;

namespace BabyCareX.Domain.Entities
{
    public class Schedule : BaseEntity
    {
        public required int FamilyId { get; set; }
        public Family? Family { get; set; }
        public required int BabaId { get; set; }
        public Baba? Baba { get; set; }
        public required int TimesAWeek { get; set; }
    }
}