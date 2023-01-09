using Alphonse.WebApi.Dto;
using Alphonse.WebApi.Extensions;
using Alphonse.WebApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace Alphonse.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;
    public UsersController(IUserService userService)
    {
        this._userService = userService;
    }

    // GET: api/Users/
    [HttpGet()]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetAll()
    {
        // TODO add some better search options
        var users = await this._userService.GetAllUsersAsync();
        return this.Ok(users.ToListDto());
    }

    // GET: api/Users/5
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetUser([FromRoute] int id)
    {
        try
        {
            var user = await this._userService.GetRequiredUserAsync(id);
            return this.Ok(user);
        }
        catch (Exception)
        {
            return this.NotFound();
        }
    }

    // POST api/Users/
    [HttpPost()]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UserDto))]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult> CreateUser([FromBody] CreateUserDto createUser)
    {
        try
        {
            var user = await this._userService.CreateAsync(createUser.Name, createUser.Pass, Models.AccessRights.Default);
            return this.CreatedAtAction(nameof(GetUser), new { id = user.Id }, user.ToDto());
        }
        catch (Exception)
        {
            return this.Conflict();
        }
    }

    // PATCH api/Users/5/pass
    [HttpPatch("{id}/pass")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult> UpdatePass([FromRoute] int id, [FromBody] string newPass)
    {
        var user = await this._userService.GetUserAsync(id);
        if (user is null)
            return this.NotFound();

        try
        {
            user = await this._userService.UpdatePasswordAsync(user.Name, newPass);
            return this.Ok(user.ToDto());
        }
        catch (Exception)
        {
            return this.Conflict();
        }
    }


    // PATCH api/Users/5/rights
    [HttpPatch("{id}/rights")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult> UpdateRights([FromRoute] int id, [FromBody] IEnumerable<string> newRights)
    {
        var user = await this._userService.GetUserAsync(id);
        if (user is null)
            return this.NotFound();

        try
        {
            var rights = newRights.ToModel();
            user = await this._userService.UpdateRightsAsync(user.Name, rights);
            return this.Ok(user.ToDto());
        }
        catch (Exception)
        {
            return this.Conflict();
        }
    }

    // DELETE api/Users/5
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteUser([FromRoute] int id)
    {
        try
        {
            var user = await this._userService.GetRequiredUserAsync(id);
            await this._userService.DeleteAsync(user.Name);
            return this.NoContent();
        }
        catch (Exception)
        {
            return this.NotFound();
        }
    }
}
