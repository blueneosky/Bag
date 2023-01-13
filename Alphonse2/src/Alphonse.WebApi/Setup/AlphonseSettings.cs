namespace Alphonse.WebApi.Setup;

public class AlphonseSettings
{
    public int? ListenPort { get; set; }
    public bool WithoutAuthorization { get; set; }
    public string? JwtSecretKey { get; set; }
    public string? JwtIssuer { get; set; }
    public string? JwtAudience { get; set; }
    public string? DataDirPath { get; set; }
    public string? DbPath { get; set; }
    public string? FallbackAdminUserName { get; set; }
    public string? FallbackAdminUserPass { get; set; }
    public string? AlphonseListenerUserName { get; set; }
    public string? AlphonseListenerUserPass { get; set; }
    public bool WithKestrelConnectionLogging { get; set; }
    public bool ForceSwagger { get; set; }
}