using Alphonse.WebApi.Models;

namespace Alphonse.WebApi.Services;

public interface ISessionService
{
    Task<SessionModel?> GetAsync(string? sessionId);
    Task<SessionModel> GetRequiredAsync(string? sessionId);
    Task<SessionModel> CreateAsync(string? name, string? pass);
    Task CloseAsync(string? sessionId);
    Task<SessionModel> GetAnonymousSessionAsync();
}
