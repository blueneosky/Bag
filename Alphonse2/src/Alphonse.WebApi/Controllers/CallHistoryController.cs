using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Alphonse.WebApi.Dbo;
using FluentValidation;
using Alphonse.WebApi.Services;
using Alphonse.WebApi.Authorization;
using System.Linq;
using Alphonse.WebApi.Dto;

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
    public async Task<ActionResult<CallHistoryPagedQueryResultDto>> GetCallHistories(
        [FromQuery] CallHistoryPageQueryParams pageQueryParams)
    {
        if (_context.CallHistories == null)
            return NotFound();

        IQueryable<CallHistoryDbo> baseQuery = _context.CallHistories
            .AsQueryable()
            // after
            .WhenNotNull(pageQueryParams.After,
                (q, v) => q.Where(ch => ch.Timestamp > v))
            // before
            .WhenNotNull(pageQueryParams.Before,
                (q, v) => q.Where(ch => ch.Timestamp < v))
            ;
        var query = from ch in baseQuery
                    select new
                    {
                        ch.CallHistoryId,
                        ch.Timestamp,
                        ch.UCallNumber,
                        ch.Action,
                        CallerName = _context.PhoneNumbers
                           .Where(pn => ch.UCallNumber == pn.UPhoneNumber)
                           .Select(pn => (string?)pn.Name)
                           .FirstOrDefault()
                    };

        var pagedResult = await query.ToPagedResultAsync(
            pageQueryParams,
            (ch, sp) => ch.CallerName != null && ch.CallerName.Contains(sp) || ch.UCallNumber.Contains(sp),
            i => new CallHistoryDto
            {
                CallHistoryId = i.CallHistoryId,
                Timestamp = i.Timestamp,
                UCallNumber = i.UCallNumber,
                Action = i.Action,
                CallerName = i.CallerName,
            },
            ch => ch.Timestamp);

        var result = new CallHistoryPagedQueryResultDto(pagedResult);

        return Ok(result);
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
