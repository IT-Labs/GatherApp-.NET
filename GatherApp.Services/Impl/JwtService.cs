using GatherApp.Contracts.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using GatherApp.Contracts.Constants;
using GatherApp.Contracts.Configuration;
using GatherApp.Contracts.Enums;
using GatherApp.Repositories;
using GatherApp.Contracts.Dtos;
using System.Text;

namespace GatherApp.Services.Impl
{
    public class JwtService : IJwtService
    {
        private readonly JWT _jwt;
        private readonly IUnitOfWork _unitOfWork;

        public JwtService(JWT jwt, IUnitOfWork unitOfWork)
        {
            _jwt = jwt;
            _unitOfWork = unitOfWork;
        }

        public AccessTokenGeneratorInfo GenerateToken(User user)
        {
            // token payload
            var claims = new List<Claim>
            {
                new Claim(Values.ClaimId, user.Id),
                new Claim(Values.ClaimMail, user.Email),
                new Claim(Values.ClaimFirstName, user.FirstName),
                new Claim(Values.ClaimLastName, user.LastName),
                new Claim(Values.ClaimRoleName, user.Role.RoleName),
            };

            // generate token
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.SecretKey));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _jwt.Issuer,
                Audience = _jwt.Audience,
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(float.Parse(_jwt.AccessExpiresInMinutes)),
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken = tokenHandler.CreateToken(tokenDescriptor);
            string token = tokenHandler.WriteToken(securityToken);

            var tokenInfo = new AccessTokenGeneratorInfo
            {
                Token = token,
                Expires = (DateTime)tokenDescriptor.Expires
            };

            return tokenInfo;
        }

        public Token GenerateRefreshToken(User user)
        {
            // create a token value
            var randomNumber = new byte[32];
            var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            var tokenValue = Convert.ToBase64String(randomNumber);

            var token = new Token
            {
                Value = tokenValue,
                ExpiresAt = DateTime.UtcNow.AddMinutes(double.Parse(_jwt.RefreshExpiresInMinutes)),
                Type = TokenEnum.RefreshToken,
                User = user
            };

            _unitOfWork.TokenRepository.Add(token);
            _unitOfWork.Complete();

            return token;
        }
    }
}
