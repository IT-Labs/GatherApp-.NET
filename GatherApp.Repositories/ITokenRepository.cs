using GatherApp.Contracts.Entities;

namespace GatherApp.Repositories
{
    public interface ITokenRepository : IRepository<Token>
    {
        /// <summary>
        /// Retrieves a token based on the specified token value.
        /// </summary>
        /// <param name="value">The value of the token to retrieve.</param>
        /// <returns>A <see cref="Token"/> representing the token with the specified value.</returns>
        Token GetByValue(string value);
    }
}
