using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Alphonse.WebApi.Dbo;

[Index(nameof(UPhoneNumber), Name = "Unicity_" + nameof(UPhoneNumber), IsUnique = true)]
public record PhoneNumberDbo
{
    [Key]
    public int PhoneNumberId { get; set; }

    [Unicode(false)]
    [MaxLength(30)]
    public string UPhoneNumber { get; set; } = null!;

    [Unicode(true)]
    [MaxLength(64)]
    public string Name { get; set; } = null!;

    public bool? Allowed { get; set; }
}