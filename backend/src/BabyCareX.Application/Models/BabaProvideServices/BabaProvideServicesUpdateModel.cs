using System.ComponentModel.DataAnnotations;
using BabyCareX.Domain.Enums;

namespace BabyCareX.Application.Models.BabaProvideServices
{
    public class BabaProvideServicesUpdateModel
    {
        [Required(ErrorMessage = "The kindNannyId field is required"), Range(1, 4, ErrorMessage = "Status must be between 1 and 3")]
        public EKindNanny? KindNannyId { get; set; }
        [Required(ErrorMessage = "The babaId field is required")]
        public int? BabaId { get; set; }
        [Required(ErrorMessage = "The createdAt field is required")]
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        [Required(ErrorMessage = "The statusId field is required"), Range(1, 4, ErrorMessage = "Status must be between 1 and 4")]
        public EStatus? StatusId { get; set; }
    }
}