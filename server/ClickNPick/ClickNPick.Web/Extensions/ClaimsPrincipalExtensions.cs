using System.Security.Claims;

namespace ClickNPick.Web.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static string GetId(this ClaimsPrincipal user)
        => user.FindFirstValue(ClaimTypes.NameIdentifier);

    public static string GetEmail(this ClaimsPrincipal user)
        => user.FindFirstValue(ClaimTypes.Email);

    public static bool IsAdmin(this ClaimsPrincipal user)
        => user.IsInRole(ClaimTypes.Role);
}
