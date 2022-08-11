namespace Alphonse.Listener;

public record AlphonseSettings
{
    public string? ModemPort { get; set; }
    public TimeSpan? AutoResetTime { get; set; }
    public string WebAppBaseUri { get; set; } = null!;
    public TimeSpan? BlacklistHangupDelay { get; set; }
    public TimeSpan? UnknownNumberHangupDelay { get; set; }
    public TimeSpan? PhonebookUpdateInterval { get; set; }
}
