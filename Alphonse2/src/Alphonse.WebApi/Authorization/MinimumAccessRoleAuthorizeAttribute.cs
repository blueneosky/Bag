using Microsoft.AspNetCore.Authorization;

namespace Alphonse.WebApi.Authorization;

[AttributeUsage(AttributeTargets.Method)]
internal class MinimumAccessRoleAuthorizeAttribute : AuthorizeAttribute
{
    private const string POLICY_PREFIX = MinimumAccessRolePolicyProvider.POLICY_PREFIX;

    public MinimumAccessRoleAuthorizeAttribute(params string[] accessRoles)
        => this.AccessRoles = accessRoles;

    public IEnumerable<string> AccessRoles
    {
        get => MinimumAccessRolePolicyProvider.ExtractAccessRolesFrom(this.Policy) ?? Enumerable.Empty<string>();
        set => this.Policy = MinimumAccessRolePolicyProvider.GeneratePolicyFrom(value);
    }
}
