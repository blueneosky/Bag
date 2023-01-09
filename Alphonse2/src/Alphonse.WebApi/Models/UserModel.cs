namespace Alphonse.WebApi.Models;

public class UserModel
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public AccessRights Rights { get; set; }
}
