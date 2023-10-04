namespace BabyCareX.Application.Models.Error
{
    public class ErrorHandling
    {
        public required int Status { get; set; }
        public required string DeveloperMessage { get; set; }
        public required string UserMessage { get; set; }
        public string? ErrorCode { get; set; }
        public string? MoreInfo { get; set; }
    }
}