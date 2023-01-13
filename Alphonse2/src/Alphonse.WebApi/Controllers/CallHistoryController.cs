using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Alphonse.WebApi.Dbo;
using FluentValidation;
using Alphonse.WebApi.Services;
using Alphonse.WebApi.Authorization;

namespace Alphonse.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CallHistoryController : ControllerBase
{
    private readonly AlphonseDbContext _context;

    public CallHistoryController(AlphonseDbContext context)
    {
        _context = context;
    }

    // GET: api/CallHistory[?after='this_date'][&before='this_date'][&pageSize=10&pageIndex=0][&reverseOrder=1]
    [HttpGet]
    [MinimumAccessRoleAuthorize(AccessRoleService.ROLE_USER)]
    public async Task<ActionResult<IEnumerable<CallHistoryDbo>>> GetCallHistories(
        [FromQuery] DateTime? after,
        [FromQuery] DateTime? before,
        [FromQuery] int? pageSize, [FromQuery] int? pageIndex,
        [FromQuery] int? reverseOrder)
    {
        if (_context.CallHistories == null)
            return NotFound();

        IQueryable<CallHistoryDbo> query = _context.CallHistories
            .AsQueryable()
            // reverseOrder
            .When((reverseOrder ?? 0) == 0,
                q => q.OrderByDescending(ch => ch.Timestamp),
                q => q.OrderBy(ch => ch.Timestamp))
            // after
            .WhenNotNull(after,
                (q, v) => q.Where(ch => ch.Timestamp > v))
            // before
            .WhenNotNull(before,
                (q, v) => q.Where(ch => ch.Timestamp < v))
            // paged result
            .WhenTrue(pageSize.HasValue && pageIndex.HasValue,
                q => q.Skip(pageSize!.Value * pageIndex!.Value).Take(pageSize!.Value))
            ;

        return await query.ToListAsync();
    }

    // GET: api/CallHistory/5
    [HttpGet("{id}")]
    [MinimumAccessRoleAuthorize(AccessRoleService.ROLE_USER)]
    public async Task<ActionResult<CallHistoryDbo>> GetCallHistoryDbo([FromRoute] int id)
    {
        if (_context.CallHistories == null)
            return NotFound();

        var callHistoryDbo = await _context.CallHistories.FindAsync(id);

        if (callHistoryDbo == null)
            return NotFound();

        return callHistoryDbo;
    }

    // POST: api/CallHistory
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    [MinimumAccessRoleAuthorize(AccessRoleService.ROLE_ADMIN, AccessRoleService.ROLE_SERVICE_LISTENER)]
    public async Task<ActionResult<CallHistoryDbo>> PostCallHistoryDbo(
        [FromBody] CallHistoryDbo callHistoryDbo,
        [FromServices] IValidator<CallHistoryDbo> validator)
    {
        if (_context.CallHistories == null)
            return Problem("Entity set 'AlphonseDbContext.CallHistories'  is null.");

        var validationResults = validator.Validate(callHistoryDbo);
        if (!validationResults.IsValid)
            return UnprocessableEntity(validationResults.ToString());

        callHistoryDbo.CallHistoryId = 0;    // safety
        _context.CallHistories.Add(callHistoryDbo);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetCallHistoryDbo), new { id = callHistoryDbo.CallHistoryId }, callHistoryDbo);
    }

    private bool CallHistoryDboExists(int id)
        => (_context.CallHistories?.Any(e => e.CallHistoryId == id)).GetValueOrDefault();

    // PUT: api/CallHistory/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    [MinimumAccessRoleAuthorize(AccessRoleService.ROLE_ADMIN, AccessRoleService.ROLE_SERVICE_LISTENER)]
    public async Task<IActionResult> PutCallHistoryDbo(
        [FromRoute] int id,
        [FromBody] CallHistoryDbo callHistoryDbo,
        [FromServices] IValidator<CallHistoryDbo> validator)
    {
        if (id != callHistoryDbo.CallHistoryId)
            return BadRequest();

        var validationResults = validator.Validate(callHistoryDbo);
        if (!validationResults.IsValid)
            return UnprocessableEntity(validationResults.ToString());

        _context.Entry(callHistoryDbo).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!CallHistoryDboExists(id))
                return NotFound();

            throw;
        }

        return NoContent();
    }

    // DELETE: api/CallHistory/5
    [HttpDelete("{id}")]
    [MinimumAccessRoleAuthorize(AccessRoleService.ROLE_ADMIN)]
    public async Task<IActionResult> DeleteCallHistoryDbo([FromRoute] int id)
    {
        if (_context.CallHistories == null)
            return NotFound();

        var callHistoryDbo = await _context.CallHistories.FindAsync(id);
        if (callHistoryDbo == null)
            return NotFound();

        _context.CallHistories.Remove(callHistoryDbo);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
