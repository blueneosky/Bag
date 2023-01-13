using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Alphonse.WebApi.Dbo;

public record UserDbo
{
    [Key]
    public int UserId { get; set; }

    [Unicode(true)]
    [StringLength(256)]
    public string Name { get; set; } = null!;

    [Unicode(false)]
    [StringLength(32)]
    public string AccessRole { get; set; }= null!;

    [Unicode(false)]
    [StringLength(256)]
    public string HPass { get; set; } = null!;
}
