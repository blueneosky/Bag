using Alphonse.WebApi.Models;

namespace Alphonse.WebApi.Services;

public interface IUserService
{
    Task<UserModel> CreateAsync(string? name, string? pass, AccessRights rights);
    Task DeleteAsync(string? name);
    Task<(bool success, UserModel? user)> TryValidateAsync(string? name, string? pass);
    Task<UserModel> ValidateAsync(string? name, string? pass);
    Task<UserModel?> GetUserAsync(int userId);
    Task<UserModel> GetRequiredUserAsync(int userId);
    Task<UserModel?> GetUserAsync(string? name);
    Task<UserModel> GetRequiredUserAsync(string? name);
    Task<IEnumerable<UserModel>> GetAllUsersAsync(AccessRights? withRights = null);
    Task<UserModel> UpdateRightsAsync(string? name, AccessRights rights);
    Task<UserModel> UpdatePasswordAsync(string? name, string? pass);
}
