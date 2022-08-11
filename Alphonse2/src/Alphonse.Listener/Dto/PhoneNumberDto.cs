
namespace Alphonse.Listener.Dto;

public record PhoneNumberDto
{
    public int PhoneNumberId { get; set; }
    public string UPhoneNumber { get; set; } = null!;
    public string Name { get; set; } = null!;
    public bool? Allowed { get; set; }
}