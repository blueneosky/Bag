namespace Alphonse.WebApi.Dto;

public record CallHistoryDto
{
    public int CallHistoryId { get; set; }
    public DateTimeOffset Timestamp { get; set; }
    public string UCallNumber { get; set; } = null!;
    public string? CallerName { get; set; }
    public string? Action { get; set; }
}
