using System.ComponentModel.DataAnnotations.Schema;
using BabyCareX.Domain.Enums;

namespace BabyCareX.Domain.Entities
{
    public class Status : BaseEntity
    {
        public required string Description { get; set; }
        [NotMapped]
        private new EStatus StatusId { get; set; }
    }
}