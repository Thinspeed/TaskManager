using System.Security.Claims;

namespace TaskManager.Api.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static int GetUserId(this ClaimsPrincipal principal)
    {
        Claim claim = principal.FindFirst("userId")
            ?? throw new Exception("Claim userId is not found.");

        return int.TryParse(claim.Value, out int userId) 
            ? userId 
            : throw new Exception("Claim userId has invalid value.");
    }
}