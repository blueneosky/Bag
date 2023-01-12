namespace Alphonse.Listener;

public record AlphonseSettings
{
    public string? ModemPort { get; set; }
    public TimeSpan? AutoResetTime { get; set; }
    public string WebApiBaseUri { get; set; } = null!;
    public string? WebApiUserName { get; set; }
    public string? WebApiUserPass { get; set; }
    public TimeSpan? BlacklistHangupDelay { get; set; }
    public TimeSpan? UnknownNumberHangupDelay { get; set; }
    public TimeSpan? PhonebookUpdateInterval { get; set; }
}
