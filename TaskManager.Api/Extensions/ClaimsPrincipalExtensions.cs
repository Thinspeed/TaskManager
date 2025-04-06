using System.Security.Claims;
using TaskManager.Api.Exceptions;

namespace TaskManager.Api.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static int GetUserId(this ClaimsPrincipal principal)
    {
        Claim claim = principal.FindFirst(ClaimTypes.NameIdentifier)
            ?? throw new AuthException("Invalid token", "Claim user id is not found.");

        return int.TryParse(claim.Value, out int userId) 
            ? userId 
            : throw new AuthException("Invalid token", "Claim userId has invalid value.");
    }
}