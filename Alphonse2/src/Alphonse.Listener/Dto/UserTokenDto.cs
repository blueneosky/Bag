namespace Alphonse.Listener.Dto;

public record UserTokenDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Token { get; set; } = null!;
    public string AccessRole { get; set; } = null!;
}
