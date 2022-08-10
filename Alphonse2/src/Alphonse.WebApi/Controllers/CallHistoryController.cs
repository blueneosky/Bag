using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Alphonse.WebApi.Dbo;
using FluentValidation;

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
    public async Task<ActionResult<IEnumerable<CallHistoryDbo>>> GetCallHistories(
        [FromQuery] DateTime? after, [FromQuery] DateTime? before,
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
            .WhenNonNull(after,
                (q, v) => q.Where(ch => ch.Timestamp > v))
            // before
            .WhenNonNull(before,
                (q, v) => q.Where(ch => ch.Timestamp < v))
            // paged result
            .WhenTrue(pageSize.HasValue && pageIndex.HasValue,
                q => q.Skip(pageSize!.Value).Take(pageIndex!.Value))
            ;

        return await query.ToListAsync();
    }

    // GET: api/CallHistory/5
    [HttpGet("{id}")]
    public async Task<ActionResult<CallHistoryDbo>> GetCallHistoryDbo(int id)
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
    public async Task<ActionResult<CallHistoryDbo>> PostCallHistoryDbo(CallHistoryDbo callHistoryDbo,
        [FromServices] IValidator<CallHistoryDbo> validator)
    {
        if (_context.CallHistories == null)
            return Problem("Entity set 'AlphonseDbContext.CallHistories'  is null.");

        var validationResults = validator.Validate(callHistoryDbo);
        if(!validationResults.IsValid)
            return UnprocessableEntity(validationResults.ToString());

        _context.CallHistories.Add(callHistoryDbo);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetCallHistoryDbo", new { id = callHistoryDbo.CallHistoryId }, callHistoryDbo);
    }

    private bool CallHistoryDboExists(int id)
    => (_context.CallHistories?.Any(e => e.CallHistoryId == id)).GetValueOrDefault();

#if DEBUG

    // PUT: api/CallHistory/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutCallHistoryDbo(int id, CallHistoryDbo callHistoryDbo,
        [FromServices] IValidator<CallHistoryDbo> validator)
    {
        if (id != callHistoryDbo.CallHistoryId)
            return BadRequest();

        var validationResults = validator.Validate(callHistoryDbo);
        if(!validationResults.IsValid)
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
    public async Task<IActionResult> DeleteCallHistoryDbo(int id)
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

#endif
}