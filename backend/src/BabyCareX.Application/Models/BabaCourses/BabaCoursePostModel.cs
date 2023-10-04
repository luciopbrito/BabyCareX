using System.ComponentModel.DataAnnotations;

namespace BabyCareX.Application.Models.BabaCourses
{
    public class BabaCoursePostModel
    {
        [Required(ErrorMessage = "The babaId field is required")]
        public int? BabaId { get; set; }
        [Required(ErrorMessage = "The name field is required"), MinLength(3)]
        public string? Name { get; set; }
        [Required(ErrorMessage = "The startPeriod field is required")]
        public DateTime? StartPeriod { get; set; }
        public DateTime? EndPeriod { get; set; }
        [Required(ErrorMessage = "The isCompleted field is required")]
        public bool? IsCompleted { get; set; }
    }
}