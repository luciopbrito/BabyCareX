namespace BabyCareX.Domain.Entities
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int StatusId { get; set; }
        public Status Status { get; set; }
    }
}