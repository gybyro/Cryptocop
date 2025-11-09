using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Cryptocop.Software.API.Extensions;

public static class ClaimValidation
{
    public static string? GetUserEmail(this ClaimsPrincipal principal)
    {
        return principal.FindFirstValue(ClaimTypes.Email) ?? principal.FindFirstValue(JwtRegisteredClaimNames.Email);
    }

    public static int? GetTokenId(this ClaimsPrincipal principal)
    {
        var tokenIdValue = principal.FindFirstValue(JwtRegisteredClaimNames.Sid);
        return int.TryParse(tokenIdValue, out var tokenId) ? tokenId : null;
    }
}