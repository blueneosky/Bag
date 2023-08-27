using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Alphonse.WebApi.Dto;
using Alphonse.WebApi.Services;
using Alphonse.WebApi.Setup;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Alphonse.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SecurityController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly AlphonseSettings _alphonseSettings;

    public SecurityController(
        IUserService userService,
        IOptions<AlphonseSettings> alphonseSettings)
    {
        this._userService = userService;
        this._alphonseSettings = alphonseSettings.Value;
    }

    [HttpGet]
    [Route("currentUser")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserTokenDto))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<UserTokenDto>> GetCurrentUserAsync()
    {
        if (_alphonseSettings.WithoutAuthorization)
        {
            return new UserTokenDto
            {
                Name = "Anonymous",
                Id = -1,
                AccessRole = AccessRoleService.ROLE_ADMIN,
                Token = "",
            };
        }

        var currentUserName = this.HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;
        var currentUserId = int.TryParse(this.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out var value) ? value : (int?)null;
        var currentUserAccessRole = this.HttpContext.User.FindFirst(ClaimTypes.Role)?.Value;
        var currentUserToken = await HttpContext.GetTokenAsync("access_token");

        if (currentUserName is null
            || currentUserId is null
            || currentUserAccessRole is null
            || currentUserToken is null)
        {
            return this.Unauthorized();
        }

        return new UserTokenDto
        {
            Name = currentUserName,
            Id = currentUserId.Value,
            AccessRole = currentUserAccessRole,
            Token = currentUserToken,
        };
    }

    [HttpPost]
    [Route("login")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserTokenDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UserTokenDto>> LoginAsync([FromBody] CredentialsDto info)
    {
        var (validated, user) = await this._userService.TryValidateAsync(info.UserName, info.UserPass);
        if (!validated)
            return this.NotFound();

        var result = new UserTokenDto
        {
            Id = user.Id,
            Name = user.Name,
            AccessRole = user.AccessRole,
            Token = GenerateToken(),
        };

        return result;

        //=========================================================

        string GenerateToken()
        {
            var key = Encoding.UTF8.GetBytes(this._alphonseSettings.JwtSecretKey!);
            var issuer = this._alphonseSettings.JwtIssuer;
            var audience = this._alphonseSettings.JwtAudience;

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                //new Claim(JwtRegisteredClaimNames.Sub, user.Name),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Role, user.AccessRole),
            };

            var expires = DateTime.Now.AddDays(1);

            var token = new JwtSecurityToken(

                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: expires,
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
