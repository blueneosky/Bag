using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alphonse.WebApi.Setup;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace Alphonse.WebApi.Authorization;

internal class MinimumAccessRolePolicyProvider : IAuthorizationPolicyProvider
{
    public const string POLICY_PREFIX = "MinimumAccessRoles:";

    private readonly ILogger<MinimumAccessRolePolicyProvider> _logger;
    private readonly IAuthorizationPolicyProvider _fallbackPolicyProvider;
    private readonly AlphonseSettings _alphonseSettings;

    public MinimumAccessRolePolicyProvider(
        ILogger<MinimumAccessRolePolicyProvider> logger,
        IOptions<AuthorizationOptions> options,
        IOptions<AlphonseSettings> alpshoneSettings)
    {
        this._logger = logger;
        this._fallbackPolicyProvider = new DefaultAuthorizationPolicyProvider(options);
        this._alphonseSettings = alpshoneSettings.Value;
    }

    public Task<AuthorizationPolicy> GetDefaultPolicyAsync()
        => _fallbackPolicyProvider.GetDefaultPolicyAsync();

    public Task<AuthorizationPolicy?> GetFallbackPolicyAsync()
        => _fallbackPolicyProvider.GetFallbackPolicyAsync();

    public Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
    {
        if (!policyName.StartsWith(POLICY_PREFIX))
            return _fallbackPolicyProvider.GetPolicyAsync(policyName);

        var accessRoles = ExtractAccessRolesFrom(policyName);
        if (accessRoles is null)
        {
            this._logger.LogWarning("Fail to extract Access roles from policy name 'Policy'", policyName);
            return _fallbackPolicyProvider.GetPolicyAsync(policyName);
        }

        var policy = new AuthorizationPolicyBuilder()
            .AddRequirements(new MinimumAccessRoleRequirement(accessRoles))
            .Build();
            
        return Task.FromResult<AuthorizationPolicy?>(policy);
    }

    public static IEnumerable<string>? ExtractAccessRolesFrom(string? policyName)
        => policyName?.Substring(POLICY_PREFIX.Length).Split(",", StringSplitOptions.RemoveEmptyEntries);

    public static string GeneratePolicyFrom(IEnumerable<string> roles)
        => $"{POLICY_PREFIX}{string.Join(",", roles)}";
}
