namespace Alphonse.WebApi.Models;

public class UserModel
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string AccessRole { get; set; } = null!;
}
