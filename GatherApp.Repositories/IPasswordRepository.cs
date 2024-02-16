using GatherApp.Contracts.Entities;

namespace GatherApp.Repositories
{
    public interface IPasswordRepository : IRepository<PasswordToken>
    {
        /// <summary>
        /// Retrieves a password reset token based on the specified token string.
        /// </summary>
        /// <param name="token">The token string used to identify the password reset request.</param>
        /// <returns>A <see cref="PasswordToken"/> representing the password reset token.</returns>
        public PasswordToken GetByToken(string token);
    }
}