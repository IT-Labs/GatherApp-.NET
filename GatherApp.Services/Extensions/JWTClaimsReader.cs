using System.Security.Claims;

namespace GatherApp.Services.Extensions
{
    public static class JWTClaimsReader
    {
        public static string HandleJWTClaims(string claimType)
        {
            var claims = ((ClaimsIdentity)Thread.CurrentPrincipal.Identity).Claims;

            return claims.FirstOrDefault(claim => claim.Type == claimType).Value;
        }
    }
}
