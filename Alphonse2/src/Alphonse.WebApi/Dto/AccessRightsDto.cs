namespace Alphonse.WebApi.Dto;

public record AccessRightsDto
{
    public string? Alias { get; set; }
    public string[] DetailedValues { get; set; } = null!;
}
