namespace BabyCareX.Application.Models.Error
{
    public class ErrorModelHandling
    {
        public required int Status { get; set; }
        public required object DeveloperMessage { get; set; }
        public required object Errors { get; set; }
        public required string UserMessage { get; set; }
        public string? ErrorCode { get; set; }
        public string? MoreInfo { get; set; }
    }
}