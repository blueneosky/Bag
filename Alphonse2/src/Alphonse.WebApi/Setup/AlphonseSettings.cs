namespace Alphonse.WebApi.Setup;

public class AlphonseSettings
{
    public int? ListenPort { get; set; }
    public string? DataBasePath { get; set; }
    public string? DbPath { get; set; }
    public bool WithKestrelConnectionLogging { get; set; }
}