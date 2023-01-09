using System.Diagnostics;
using Alphonse.WebApi.Dbo;
using Alphonse.WebApi.Extensions;
using Alphonse.WebApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Alphonse.WebApi.Services;

public class UserService : IUserService
{
    private readonly ILogger<UserService> _logger;
    private readonly AlphonseDbContext _context;
    private readonly IPasswordHasher<string> _passwordHasher;

    public UserService(
        ILogger<UserService> logger,
        AlphonseDbContext context,
        IPasswordHasher<string> passwordHasher)
    {
        this._logger = logger;
        this._context = context;
        this._passwordHasher = passwordHasher;
    }

    public async Task<UserModel> CreateAsync(string? name, string? pass, AccessRights rights)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name));
        if (string.IsNullOrWhiteSpace(pass))
            throw new ArgumentException($"'{nameof(pass)}' cannot be null or whitespace.", nameof(pass));


        if (await this._context.Users.AnyAsync(u => u.Name == name))
            throw new ArgumentException($"User '{name}' already exist", nameof(name));

        var hpass = this._passwordHasher.HashPassword(name, pass);
        var user = new UserDbo()
        {
            Name = name,
            Rights = (long)rights,
            HPass = hpass,
        };

        await this._context.Users.AddAsync(user);
        await this._context.SaveChangesAsync();

        return user.ToModel();
    }

    public async Task DeleteAsync(string? name)
    {
        var user = await GetRequiredUserDboAsync(name).ConfigureAwait(false);

        this._context.Users.Remove(user);
        await this._context.SaveChangesAsync();
    }

    public async Task<(bool success, UserModel user)> TryValidateAsync(string? name, string? pass)
    {
        var user = await ValidateAsync(name, pass, false);
        return (user is not null, user!);
    }

    public async Task<UserModel> ValidateAsync(string? name, string? pass)
    {
        var user = await ValidateAsync(name, pass, true);
        return user ?? throw new InvalidOperationException("unexpected null");
    }

    private async Task<UserModel?> ValidateAsync(string? name, string? pass, bool required)
    {
        var user = await GetUserDboAsync(name, required).ConfigureAwait(false);
        if (user is null)
            return null;

        var result = this._passwordHasher.VerifyHashedPassword(user.Name, user.HPass, pass);

        if (result == PasswordVerificationResult.Failed)
            return required ? throw new ArgumentException("Invalid password", nameof(pass)) : null;

        if (result == PasswordVerificationResult.Success)
            return user.ToModel();

        Debug.Assert(result == PasswordVerificationResult.SuccessRehashNeeded);
        try
        {
            this._logger.LogInformation($"Rehash password needed for user '{name} ...'");
            user.HPass = this._passwordHasher.HashPassword(user.Name, pass);
            await this._context.SaveChangesAsync();
            this._logger.LogInformation($"Rehash password needed for user '{name}' DONE");
        }
        catch (Exception e)
        {
            this._logger.LogWarning(e, $"Rehash password failed for user '{name}'");
        }

        return user.ToModel();
    }

    public async Task<IEnumerable<UserModel>> GetAllUsersAsync(AccessRights? withRights = null)
    {
        var users = await this._context.Users
            .AsQueryable()
            .WhenNonNull((long?)withRights, (query, right) => query.Where(u => (u.Rights & right) == right))
            .ToListAsync();

        return users.ToModel();
    }

    public async Task<UserModel> UpdateRightsAsync(string? name, AccessRights rights)
    {
        var user = await this.GetRequiredUserDboAsync(name);
        user.Rights = (long)rights;
        await this._context.SaveChangesAsync();

        return user.ToModel();
    }

    public async Task<UserModel> UpdatePasswordAsync(string? name, string? pass)
    {
        var user = await this.GetRequiredUserDboAsync(name);
        user.HPass = this._passwordHasher.HashPassword(name!, pass);
        await this._context.SaveChangesAsync();

        return user.ToModel();
    }

    public async Task<UserModel?> GetUserAsync(int id)
    {
        var user = await this.GetUserDboAsync(id, false);
        return user?.ToModel();
    }

    public async Task<UserModel?> GetUserAsync(string? name)
    {
        var user = await this.GetUserDboAsync(name, false);
        return user?.ToModel();
    }

    public async Task<UserModel> GetRequiredUserAsync(int id)
    {
        var user = await GetRequiredUserDboAsync(id);
        return user.ToModel();
    }

    public async Task<UserModel> GetRequiredUserAsync(string? name)
    {
        var user = await GetRequiredUserDboAsync(name);
        return user.ToModel();
    }

    private async Task<UserDbo> GetRequiredUserDboAsync(string? name)
        => (await GetUserDboAsync(name, true)) ?? throw new InvalidOperationException("unexpected null");

    private async Task<UserDbo?> GetUserDboAsync(string? name, bool required)
    {
        if (string.IsNullOrWhiteSpace(name))
            return required ? throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name)) : null;

        var user = await this._context.Users.FirstOrDefaultAsync(u => u.Name == name);
        if (user is null)
            return required ? throw new ArgumentException($"User '{name}' don't exist", nameof(name)) : null;

        return user;
    }

    private async Task<UserDbo> GetRequiredUserDboAsync(int id)
        => (await GetUserDboAsync(id, true)) ?? throw new InvalidOperationException("unexpected null");

    private async Task<UserDbo?> GetUserDboAsync(int id, bool required)
    {
        var user = await this._context.Users.FirstOrDefaultAsync(u => u.UserId == id);
        if (user is null)
            return required ? throw new ArgumentException($"User id '{id}' don't exist", nameof(id)) : null;

        return user;
    }
}
