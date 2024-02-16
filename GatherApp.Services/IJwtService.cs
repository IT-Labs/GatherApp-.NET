using GatherApp.Contracts.Dtos;
using GatherApp.Contracts.Entities;

namespace GatherApp.Services
{
    public interface IJwtService
    {
        /// <summary>
        /// Generates an access token for the provided user.
        /// </summary>
        /// <param name="user">The user for whom to generate the access token.</param>
        /// <returns>An <see cref="AccessTokenGeneratorInfo"/> containing the generated access token information.</returns>
        AccessTokenGeneratorInfo GenerateToken(User user);

        /// <summary>
        /// Generates a refresh token for the provided user.
        /// </summary>
        /// <param name="user">The user for whom to generate the refresh token.</param>
        /// <returns>A <see cref="Token"/> representing the generated refresh token.</returns>
        Token GenerateRefreshToken(User user);

    }
}
