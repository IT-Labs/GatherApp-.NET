using GatherApp.Contracts.Constants;
using GatherApp.Contracts.Enums;
using Microsoft.AspNetCore.Http;

namespace GatherApp.Services.Extensions
{
    public static class CookieHandler
    {
        public static void GenerateRefreshTokenCookie(string value, DateTime expires, IHttpContextAccessor context, string domain)
        {
            CookieOptions cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = expires,
                // Domain property should not be assigned when working in local environment, just comment it out
                //Domain = domain,
                Path = "/",
            };

            context.HttpContext.Response.Cookies.Append(TokenEnum.RefreshToken.ToString(), value, cookieOptions);
        }
    }
}
