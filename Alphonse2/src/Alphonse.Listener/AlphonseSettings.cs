namespace Alphonse.Listener;

public record AlphonseSettings
{
    public string? ModemPort { get; set; }
    public TimeSpan? ResetTime { get; set; }
} 
