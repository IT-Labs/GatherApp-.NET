using GatherApp.Contracts.Configuration;
using GatherApp.Contracts.Constants;
using GatherApp.Services;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace GatherApp.API.Middleware
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly JWT _jwt;

        public AuthenticationMiddleware(RequestDelegate next, JWT jwt)
        {
            _next = next;
            _jwt = jwt;
        }
        public async Task Invoke(HttpContext context, IJwtService jwtService)
        {
            string token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if ( token != null)
            {
                AttachAccountToContext(context, token);
            }

            await _next(context);
        }

        private void AttachAccountToContext(HttpContext context, string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(_jwt.SecretKey);

                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = _jwt.Issuer,
                    ValidAudience = _jwt.Audience,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };

                var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var validatedToken);
                context.User = principal;
                Thread.CurrentPrincipal = principal;

                var validToken = validatedToken as JwtSecurityToken;
                if (validToken != null)
                {
                    context.Items[Values.ClaimId] = validToken.Claims.First(x => x.Type == "id").Value;
                    context.Items[Values.ClaimFirstName] = validToken.Claims.First(x => x.Type == "first_name").Value;
                    context.Items[Values.ClaimLastName] = validToken.Claims.First(x => x.Type == "last_name").Value;
                    context.Items[Values.ClaimMail] = validToken.Claims.First(x => x.Type == "email").Value;
                    context.Items[Values.ClaimRoleName] = validToken.Claims.First(x => x.Type == "role_name").Value;
                }
            } catch
            {
                // do nothing
                // token validation fails, request will remain unauthenticated
            }
        }
    }
}
