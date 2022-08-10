using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Alphonse.WebApi.Dbo;

public class CallHistoryDbo
{
    [Key]
    public int CallHistoryId { get; set; }

    public DateTimeOffset Timestamp { get; set; }

    [Unicode(false)]
    [MaxLength(30)]
    public string UCallNumber { get; set; } = null!;
}