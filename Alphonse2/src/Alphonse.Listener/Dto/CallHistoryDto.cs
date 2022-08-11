
namespace Alphonse.Listener.Dto;

public record CallHistoryDto
{
    public int CallHistoryId { get; set; }

    public DateTimeOffset Timestamp { get; set; }

    public string UCallNumber { get; set; } = null!;
}