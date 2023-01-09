namespace Alphonse.WebApi.Models;

public class SessionModel
{
    public string Id { get; set; } = null!;
    public UserModel User { get; set; } = null!;
}