using System.ComponentModel.DataAnnotations;

namespace BabyCareX.Application.Models.Schedules
{
    public class SchedulePostModel
    {
        [Required(ErrorMessage = "The familyId field is required")]
        public int? FamilyId { get; set; }
        [Required(ErrorMessage = "The babaId field is required")]
        public int? BabaId { get; set; }
        [Required(ErrorMessage = "The timesAWeek field is required"), Range(1, 7, ErrorMessage = "The times a week must be a maximum of 7 days.")]
        public int? TimesAWeek { get; set; }
    }
}