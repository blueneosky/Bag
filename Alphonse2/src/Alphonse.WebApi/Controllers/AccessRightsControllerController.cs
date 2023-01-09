using Alphonse.WebApi.Dto;
using Alphonse.WebApi.Extensions;
using Alphonse.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace Alphonse.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccessRightsController : ControllerBase
{
    // GET: api/AccessRights/
    [HttpGet()]
    public Task<ActionResult<IEnumerable<AccessRightsDto>>> GetAll()
    {
        return Task.FromResult(Get());

        //====================================================

        ActionResult<IEnumerable<AccessRightsDto>> Get()
        {
            var rightsArray = Enum.GetValues<AccessRights>();
            return this.Ok(rightsArray.ToListDto());
        }
    }

    // GET: api/AccessRights/5
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public Task<ActionResult> GetAccessRights([FromRoute] string id)
    {
        return Task.FromResult(Get());

        //====================================================

        ActionResult Get()
            => Enum.TryParse<AccessRights>(id, out var rights)
                ? this.Ok(rights.ToDto())
                : this.NotFound();
    }
}

