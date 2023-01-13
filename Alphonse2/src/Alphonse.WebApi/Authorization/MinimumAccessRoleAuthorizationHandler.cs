using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Alphonse.WebApi.Services;
using Alphonse.WebApi.Setup;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace Alphonse.WebApi.Authorization;

internal class MinimumAccessRoleAuthorizationHandler : AuthorizationHandler<MinimumAccessRoleRequirement>
{
    private readonly ILogger<MinimumAccessRoleAuthorizationHandler> _logger;
    private readonly AlphonseSettings _alpshoneSettings;
    private readonly IUserService _userService;

    public MinimumAccessRoleAuthorizationHandler(
        ILogger<MinimumAccessRoleAuthorizationHandler> logger,
        IOptions<AlphonseSettings> alpshoneSettings,
        IUserService userService)
    {
        this._logger = logger;
        this._alpshoneSettings = alpshoneSettings.Value;
        this._userService = userService;
    }

    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumAccessRoleRequirement requirement)
    {
        if (!requirement.AccessRoles.Any()
            || _alpshoneSettings.WithoutAuthorization)
        {
            // no requirement or authorization turned off
            context.Succeed(requirement);
            return Task.CompletedTask;
        }

        return HandleAsync();

        //=============================================================================

        async Task HandleAsync()
        {
            var claimedUserName = context.User.FindFirst(ClaimTypes.Name)?.Value;
            var user = await this._userService.GetUserAsync(claimedUserName).ConfigureAwait(false);
            if (user is null)
                return; // sory bro, I don't know who you are ? (delete account)

            if (requirement.Satisfy(user.AccessRole))
                context.Succeed(requirement);
        }
    }
}
