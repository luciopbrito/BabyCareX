using System.ComponentModel.DataAnnotations;
using BabyCareX.Domain.Entities;
using BabyCareX.Domain.Enums;

namespace BabyCareX.Application.Models.BabaCapacities
{
    public class BabaCapacityUpdateModel
    {
        [Required(ErrorMessage = "The name field is required"), MinLength(3)]
        public string? Name { get; set; }
        [Required(ErrorMessage = "The babaId field is required")]
        public int? BabaId { get; set; }
        [Required(ErrorMessage = "The createdAt field is required")]
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        [Required(ErrorMessage = "The statusId field is required"), Range(1, 4, ErrorMessage = "Status must be between 1 and 4")]
        public EStatus? StatusId { get; set; }
        public Baba? Baba { get; set; }
    }
}