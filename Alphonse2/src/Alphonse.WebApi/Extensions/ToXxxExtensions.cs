using System;
using System.Collections.Generic;
using System.Linq;
using Alphonse.WebApi.Dbo;
using Alphonse.WebApi.Dto;
using Alphonse.WebApi.Models;

namespace Alphonse.WebApi.Extensions;

public static class ToXxxExtensions
{
    public static SessionDto ToDto(this SessionModel model) => new()
    {
        Id = model.Id,
        User = model.User.ToDto(),
    };

    public static UserDto ToDto(this UserModel model) => new()
    {
        Id = model.Id,
        Name = model.Name,
        Rights = model.Rights.ToDto(),
    };

    public static List<UserDto> ToListDto(this IEnumerable<UserModel> models)
        => models.Select(ToDto).ToList();


    private static readonly IReadOnlyDictionary<long, string> constAccessRights = Enum.GetValues<AccessRights>()
        .Where(v => v != AccessRights.None)
        .Distinct()
        .ToDictionary(v => (long)v, v => v.ToString());

    public static AccessRightsDto ToDto(this AccessRights rights)
    {
        string? alias = rights.ToString();
        if (alias.Split(',').Count() != 1)
            alias = null;
        var detailedValues = Enumerable.Range(0, sizeof(long) * 8)
            .Select(i => (long)rights & (1L << i))
            .Where(v => v != 0)
            .Select(v => constAccessRights.TryGetValue(v, out var name) ? name : v.ToString())
            .ToArray();

        return new()
        {
            Alias = alias,
            DetailedValues = detailedValues,
        };
    }

    public static List<AccessRightsDto> ToListDto(this IEnumerable<AccessRights> rights)
        => rights.Select(ToDto).ToList();

    public static AccessRights ToModel(this IEnumerable<string> rightsDto)
        => rightsDto.Aggregate(AccessRights.None, (r, n) => r | Enum.Parse<AccessRights>(n));

    public static UserModel ToModel(this UserDbo dbo) => new()
    {
        Id = dbo.UserId,
        Name = dbo.Name,
        Rights = (AccessRights)dbo.Rights,
    };

    public static IEnumerable<UserModel> ToModel(this IEnumerable<UserDbo> dbos)
        => dbos.Select(ToModel);

    public static UserDbo ToDbo(this UserModel model) => new()
    {
        UserId = model.Id,
        Name = model.Name,
        Rights = (long)model.Rights,
    };
}
