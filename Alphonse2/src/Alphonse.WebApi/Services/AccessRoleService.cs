namespace Alphonse.WebApi.Services;

public static class AccessRoleService
{
    public const string ROLE_ADMIN = "admin";
    public const string ROLE_SERVICE_LISTENER = "service_listeber";
    public const string ROLE_USER = "user";

    public static IReadOnlyCollection<string> AllRoles => new[] { ROLE_ADMIN, ROLE_USER, ROLE_SERVICE_LISTENER };

    public static bool IsPartOf(string role, string forRole)
        => role == forRole || (role, forRole) switch
        {
            (ROLE_USER, ROLE_ADMIN) => true,
            (ROLE_SERVICE_LISTENER, ROLE_ADMIN) => true,
            _ => false,
        };
}
