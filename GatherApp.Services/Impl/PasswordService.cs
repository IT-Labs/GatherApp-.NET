namespace GatherApp.Services.Impl
{
    public class PasswordService : IPasswordService
    {
        public string HashPassword(string password)
        {
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
            return new string(hashedPassword);
        }

        public bool VerifyPassword(string requestPassword, string userPasswordInDatabase)
        {
            return BCrypt.Net.BCrypt.Verify(requestPassword, userPasswordInDatabase);
        }
    }
}
