using System.ComponentModel.DataAnnotations;
using BabyCareX.Domain.Enums;

namespace BabyCareX.Application.Models.Schedules
{
    public class ScheduleUpdateModel
    {
        [Required(ErrorMessage = "The familyId field is required")]
        public int? FamilyId { get; set; }
        [Required(ErrorMessage = "The babaId field is required")]
        public int? BabaId { get; set; }
        [Required(ErrorMessage = "The timesAWeek field is required"), Range(1, 7, ErrorMessage = "The times a week must be a maximum of 7 days.")]
        public int? TimesAWeek { get; set; }
        [Required(ErrorMessage = "The statusId field is required"), Range(1, 4, ErrorMessage = "Status must be between 1 and 4")]
        public EStatus? StatusId { get; set; }
        [Required(ErrorMessage = "The createdAt field is required")]
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}