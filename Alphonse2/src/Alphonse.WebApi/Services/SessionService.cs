using Alphonse.WebApi.Extensions;
using Alphonse.WebApi.Models;
using Alphonse.WebApi.Setup;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace Alphonse.WebApi.Services;

public class SessionService : ISessionService
{
    private static readonly string constAnonymousSessionId = Guid.NewGuid().ToString("N");

    private readonly IMemoryCache _memoryCache;
    private readonly IUserService _userService;
    private readonly AlphonseSettings _alphonseSettings;

    public SessionService(
        IMemoryCache memoryCache,
        IUserService userService,
        IOptions<AlphonseSettings> alphonseSettings
        )
    {
        this._memoryCache = memoryCache;
        this._userService = userService;
        this._alphonseSettings = alphonseSettings.Value;
    }

    public async Task<SessionModel> CreateAsync(string? name, string? pass)
    {
        var user = await this._userService.ValidateAsync(name, pass);
        return await this.Open(user);
    }

    private Task<SessionModel> Open(UserModel user)
    {
        var sessionId = Guid.NewGuid().ToString("N");

        var session = new SessionModel
        {
            Id = sessionId,
            User = user,
        };

        var cacheEntry = this._memoryCache.CreateEntry(sessionId);
        cacheEntry.SetValue(session);
        cacheEntry.SetSize(1);
        cacheEntry.SetSlidingExpiration(TimeSpan.FromMinutes(30));

        return Task.FromResult(session);
    }

    public Task<SessionModel?> GetAsync(string? sessionId)
        => this.GetAsync(sessionId, false);

    public async Task<SessionModel> GetRequiredAsync(string? sessionId)
        => (await this.GetAsync(sessionId, true)) ?? throw new InvalidOperationException("Unexpected null");

    private Task<SessionModel?> GetAsync(string? sessionId, bool required)
    {
        return Task.FromResult(Get());

        //===================================================

        SessionModel? Get()
        {
            if (string.IsNullOrWhiteSpace(sessionId))
                return required ? throw new ArgumentException($"'{nameof(sessionId)}' cannot be null or whitespace.", nameof(sessionId)) : null;

            if (!this._memoryCache.TryGetValue(sessionId, out SessionModel session))
                return required ? throw new ArgumentException($"Session '{sessionId}' not found", nameof(sessionId)) : null;

            return session;
        }
    }

    public Task CloseAsync(string? sessionId)
    {
        if (string.IsNullOrWhiteSpace(sessionId))
            throw new ArgumentException($"'{nameof(sessionId)}' cannot be null or whitespace.", nameof(sessionId));

        if (!this._memoryCache.TryGetValue(sessionId, out var _))
            throw new ArgumentException($"Session '{sessionId}' not found", nameof(sessionId));

        this._memoryCache.Remove(sessionId);

        return Task.CompletedTask;
    }

    public Task<SessionModel> GetAnonymousSessionAsync()
    {
        var result = this._memoryCache.GetOrCreate<SessionModel>(constAnonymousSessionId, CreateAnonymousSession);
        return Task.FromResult(result);

        //=====================================================

        SessionModel CreateAnonymousSession(ICacheEntry cacheEntry)
        {
            var user = new UserModel
            {
                Id = -1,
                Name = "__anonymous__",
                Rights = this._alphonseSettings.AnonymousUserRights?.ToModel() ?? AccessRights.None,
            };
            var session = new SessionModel{
                Id = (string)cacheEntry.Key,
                User = user,
            };

            cacheEntry.SetValue(session);
            // note : no sliding nor size - it's a long life entity

            return session;
        }
    }
}
