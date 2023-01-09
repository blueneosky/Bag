namespace Alphonse.WebApi.Dto;

public record AuthenticationInfoDto
{
    public string? UserName { get; set; }
    public string? UserPass { get; set; }
}
