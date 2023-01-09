using Alphonse.WebApi.Dto;
using Alphonse.WebApi.Extensions;
using Alphonse.WebApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace Alphonse.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SessionsController : ControllerBase
{
    private readonly ISessionService _sessionService;

    public SessionsController(ISessionService sessionService)
    {
        this._sessionService = sessionService;
    }

    // GET: api/Sessions/5
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SessionDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetSession([FromRoute] string sessionId)
    {
        try
        {
            var session = await this._sessionService.GetRequiredAsync(sessionId);
            return this.Ok(session);
        }
        catch (Exception)
        {
            return this.NotFound();
        }
    }

    // POST api/Sessions/
    [HttpPost()]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(SessionDto))]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> CreateSession([FromBody] AuthenticationInfoDto info)
    {
        try
        {
            var session = await this._sessionService.CreateAsync(info.UserName, info.UserPass);
            return this.CreatedAtAction(nameof(GetSession), new { id = session.Id }, session.ToDto());
        }
        catch (Exception)
        {
            return this.Conflict();
        }
    }

    // DELETE: api/Sessions/5
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> CloseSession([FromRoute] string sessionId)
    {
        try
        {
            await this._sessionService.CloseAsync(sessionId);
            return this.NoContent();
        }
        catch (Exception)
        {
            return this.NotFound();
        }
    }
}
