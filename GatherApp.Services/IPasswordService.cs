namespace GatherApp.Services
{
    public interface IPasswordService
    {
        /// <summary>
        /// Hashes the provided password for secure storage.
        /// </summary>
        /// <param name="password">The password to be hashed.</param>
        /// <returns>A string representing the hashed password.</returns>
        string HashPassword(string password);

        /// <summary>
        /// Verifies if the provided password matches the stored hashed password.
        /// </summary>
        /// <param name="requestPassword">The password to be verified.</param>
        /// <param name="userPasswordInDatabase">The stored hashed password to compare against.</param>
        /// <returns>True if the passwords match; otherwise, false.</returns>
        bool VerifyPassword(string requestPassword, string userPasswordInDatabase);
    }
}
