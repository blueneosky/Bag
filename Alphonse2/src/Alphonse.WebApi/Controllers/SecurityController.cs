using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Alphonse.WebApi.Dto;
using Alphonse.WebApi.Services;
using Alphonse.WebApi.Setup;
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
    [Route("check")]
    public Task<ActionResult<string>> CheckAsync() => Task.FromResult<ActionResult<string>>(this.Ok("OK"));

    [HttpPost]
    [Route("login")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> LoginAsync([FromBody] AuthenticationInfoDto info)
    {
        var (validated, user) = await this._userService.TryValidateAsync(info.UserName, info.UserPass);
        return validated ? this.Ok(GenerateToken()) : this.NotFound();

        //=========================================================

        string GenerateToken()
        {
            var key = Encoding.UTF8.GetBytes(this._alphonseSettings.JwtSecretKey!);
            var issuer = this._alphonseSettings.JwtIssuer;
            var audience = this._alphonseSettings.JwtAudience;

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, user.Name),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
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
