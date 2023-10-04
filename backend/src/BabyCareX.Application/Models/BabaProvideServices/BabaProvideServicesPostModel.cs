using System.ComponentModel.DataAnnotations;
using BabyCareX.Domain.Enums;

namespace BabyCareX.Application.Models.BabaProvideServices
{
    public class BabaProvideServicesPostModel
    {
        [Required(ErrorMessage = "The kindNannyId field is required"), Range(1, 4, ErrorMessage = "Status must be between 1 and 3")]
        public EKindNanny? KindNannyId { get; set; }
        [Required(ErrorMessage = "The babaId field is required")]
        public int? BabaId { get; set; }
    }
}