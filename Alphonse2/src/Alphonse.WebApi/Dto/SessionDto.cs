namespace Alphonse.WebApi.Dto;

public record SessionDto
{
    public string Id { get; set; } = null!;
    public UserDto User { get; set; } = null!;
}
