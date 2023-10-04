using BabyCareX.Domain.Enums;

namespace BabyCareX.Domain.Entities
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public virtual EStatus StatusId { get; set; }
    }
}