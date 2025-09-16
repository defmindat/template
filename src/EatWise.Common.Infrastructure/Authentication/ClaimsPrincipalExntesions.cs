using System.Diagnostics.Eventing.Reader;
using System.Security.Claims;
using EatWise.Common.Application.Exceptions;

namespace EatWise.Common.Infrastructure.Authentication;

public static class ClaimsPrincipalExntesions
{
    public static Guid GetUserId(this ClaimsPrincipal? principal)
    {
        string? userId = principal?.FindFirst(CustomClaims.Sub)?.Value;
        
        return Guid.TryParse(userId, out Guid parsedUserId) ? parsedUserId : throw new EatWiseException("User identifier is unavailable.");
    }

    public static string GetIdentityId(this ClaimsPrincipal? principal)
    {
        return principal?.FindFirst(ClaimTypes.NameIdentifier)?.Value ??
               throw new EatWiseException("User identity is unavailable");
    }

    public static HashSet<string> GetPermissions(this ClaimsPrincipal? principal)
    {
        IEnumerable<Claim> permissionClaims = principal?.FindAll(CustomClaims.Permisssion) ?? throw new EatWiseException("Permissions claim is unavailable.");
        
        return permissionClaims.Select(c => c.Value).ToHashSet();
    }
}
