namespace Alphonse.WebApi.Dto;

public record UserDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string AccessRole { get; set; } = null!;
}

public record CreateUserDto
{
    public string Name { get; set; } = null!;
    public string Pass { get; set; } = null!;
}
