using Microsoft.AspNetCore.Authorization;
using Alphonse.WebApi.Services;

namespace Alphonse.WebApi.Authorization;

internal class MinimumAccessRoleRequirement : IAuthorizationRequirement
{
    public IEnumerable<string> AccessRoles { get; }
    public MinimumAccessRoleRequirement(IEnumerable<string> accessRoles)
        => this.AccessRoles = accessRoles;

    public bool Satisfy(string accessRole)
        => this.AccessRoles.Any(r => AccessRoleService.IsPartOf(r, accessRole));
}
