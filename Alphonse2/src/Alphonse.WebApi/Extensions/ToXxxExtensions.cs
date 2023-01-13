using Alphonse.WebApi.Dbo;
using Alphonse.WebApi.Dto;
using Alphonse.WebApi.Models;
using Alphonse.WebApi.Services;

namespace Alphonse.WebApi.Extensions;

public static class ToXxxExtensions
{
    public static UserDto ToDto(this UserModel model) => new()
    {
        Id = model.Id,
        Name = model.Name,
        AccessRole = model.AccessRole,
    };

    public static List<UserDto> ToListDto(this IEnumerable<UserModel> models)
        => models.Select(ToDto).ToList();

    public static UserModel ToModel(this UserDbo dbo) => new()
    {
        Id = dbo.UserId,
        Name = dbo.Name,
        AccessRole = dbo.AccessRole,
    };

    public static IEnumerable<UserModel> ToModel(this IEnumerable<UserDbo> dbos)
        => dbos.Select(ToModel);

    public static UserDbo ToDbo(this UserModel model) => new()
    {
        UserId = model.Id,
        Name = model.Name,
        AccessRole = model.AccessRole,
    };
}
