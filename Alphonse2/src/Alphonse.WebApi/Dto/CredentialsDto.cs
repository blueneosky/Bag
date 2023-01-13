using System.ComponentModel.DataAnnotations;

namespace Alphonse.WebApi.Dto;

public record CredentialsDto
{
    [Required]
    public string UserName { get; set; } = null!;
    [Required]
    public string UserPass { get; set; } = null!;
}
