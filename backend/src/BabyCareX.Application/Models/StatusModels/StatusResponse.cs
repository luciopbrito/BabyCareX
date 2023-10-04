namespace BabyCareX.Application.Models.StatusModels
{
    public class StatusResponse
    {
        public required string Description { get; set; }
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}