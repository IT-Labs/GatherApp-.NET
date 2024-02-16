using GatherApp.Contracts.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GatherApp.Services.Extensions
{
    internal class AuthSSOToken
    {
        public static (string FirstName, string LastName, string Email, string Oid) ExtractUserDataFromIdToken(string idToken)
        {
            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(idToken);

            string name = GetClaimValue(token, "name");
            string email = GetClaimValue(token, "preferred_username");
            string oid = GetClaimValue(token, "oid");
            string[] nameParts = name.Split(' ');
            string firstName = nameParts.FirstOrDefault();
            string lastName = nameParts.LastOrDefault();

            return (firstName, lastName, email, oid);
        }

        private static string GetClaimValue(JwtSecurityToken token, string claimType)
        {
            var claim = token.Claims.FirstOrDefault(c => c.Type == claimType);
            return claim != null ? claim.Value : string.Empty;
        }
    }                                       
}
