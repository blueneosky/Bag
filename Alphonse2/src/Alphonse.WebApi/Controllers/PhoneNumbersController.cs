using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Alphonse.WebApi.Dbo;
using FluentValidation;
using Alphonse.WebApi.Authorization;
using Alphonse.WebApi.Services;
using Alphonse.WebApi.Dto;
using System.Linq.Expressions;

namespace Alphonse.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PhoneNumbersController : ControllerBase
{
    private readonly AlphonseDbContext _context;

    public PhoneNumbersController(AlphonseDbContext context)
    {
        _context = context;
    }

    // GET: api/PhoneNumbers
    [HttpGet]
    [MinimumAccessRoleAuthorize(AccessRoleService.ROLE_USER, AccessRoleService.ROLE_SERVICE_LISTENER)]
    public async Task<ActionResult<PhoneNumberPagedQueryResultDto>> GetPhoneNumbers(
        [FromQuery] PageQueryParams pageQueryParams)
    {
        if (_context.PhoneNumbers == null)
            return NotFound();

        var pagedQuery = await _context.PhoneNumbers.ToPagedResultAsync(
            pageQueryParams,
            (pn, sp) => pn.Name.Contains(sp) || pn.UPhoneNumber.Contains(sp),
            dbo => dbo,
            pn => pn.Name, pn => pn.UPhoneNumber);

        var result = new PhoneNumberPagedQueryResultDto(pagedQuery);

        return Ok(result);
    }

    // GET: api/PhoneNumbers/5
    [HttpGet("{id}")]
    [MinimumAccessRoleAuthorize(AccessRoleService.ROLE_USER, AccessRoleService.ROLE_SERVICE_LISTENER)]
    public async Task<ActionResult<PhoneNumberDbo>> GetPhoneNumberDbo([FromRoute] int id)
    {
        if (_context.PhoneNumbers == null)
            return NotFound();
        var phoneNumberDbo = await _context.PhoneNumbers.FindAsync(id);

        if (phoneNumberDbo == null)
            return NotFound();

        return phoneNumberDbo;
    }

    // GET: api/PhoneNumbers/byUPhoneNumber/+331234567
    [HttpGet("byUPhoneNumber/{uPhoneNumber}")]
    [MinimumAccessRoleAuthorize(AccessRoleService.ROLE_USER, AccessRoleService.ROLE_SERVICE_LISTENER)]
    public async Task<ActionResult<PhoneNumberDbo>> GetPhoneNumberDboByUPhoneNumber([FromRoute] string uPhoneNumber)
    {
        if (_context.PhoneNumbers == null)
            return NotFound();
        var phoneNumberDbo = await _context.PhoneNumbers
            .FirstOrDefaultAsync(pn => pn.UPhoneNumber == uPhoneNumber);

        if (phoneNumberDbo == null)
            return NotFound();

        return phoneNumberDbo;
    }

    // PUT: api/PhoneNumbers/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    [MinimumAccessRoleAuthorize(AccessRoleService.ROLE_USER)]
    public async Task<IActionResult> PutPhoneNumberDbo(
        [FromRoute] int id,
        [FromBody] PhoneNumberDbo phoneNumberDbo,
        [FromServices] IValidator<PhoneNumberDbo> validator)
    {
        if (id != phoneNumberDbo.PhoneNumberId)
            return BadRequest();

        var validationResults = validator.Validate(phoneNumberDbo);
        if (!validationResults.IsValid)
            return UnprocessableEntity(validationResults.ToString());

        _context.Entry(phoneNumberDbo).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!PhoneNumberDboExists(id))
                return NotFound();

            throw;
        }

        return NoContent();
    }

    // POST: api/PhoneNumbers
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    [MinimumAccessRoleAuthorize(AccessRoleService.ROLE_USER)]
    public async Task<ActionResult<PhoneNumberDbo>> PostPhoneNumberDbo(
        [FromBody] PhoneNumberDbo phoneNumberDbo,
        [FromServices] IValidator<PhoneNumberDbo> validator)
    {
        if (_context.PhoneNumbers == null)
            return Problem("Entity set 'AlphonseDbContext.PhoneNumbers' is null.");

        var validationResults = validator.Validate(phoneNumberDbo);
        if (!validationResults.IsValid)
            return UnprocessableEntity(validationResults.ToString());

        _context.PhoneNumbers.Add(phoneNumberDbo);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetPhoneNumberDbo), new { id = phoneNumberDbo.PhoneNumberId }, phoneNumberDbo);
    }

    // DELETE: api/PhoneNumbers/5
    [HttpDelete("{id}")]
    [MinimumAccessRoleAuthorize(AccessRoleService.ROLE_USER)]
    public async Task<IActionResult> DeletePhoneNumberDbo([FromRoute] int id)
    {
        if (_context.PhoneNumbers == null)
            return NotFound();

        var phoneNumberDbo = await _context.PhoneNumbers.FindAsync(id);
        if (phoneNumberDbo == null)
            return NotFound();

        _context.PhoneNumbers.Remove(phoneNumberDbo);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool PhoneNumberDboExists(int id)
        => (_context.PhoneNumbers?.Any(e => e.PhoneNumberId == id)).GetValueOrDefault();
}
