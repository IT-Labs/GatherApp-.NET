namespace GatherApp.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Gets the repository for managing events.
        /// </summary>
        IEventRepository EventRepository { get; }

        /// <summary>
        /// Gets the repository for managing users.
        /// </summary>
        IUserRepository UserRepository { get; }

        /// <summary>
        /// Gets the repository for managing password-related operations.
        /// </summary>
        IPasswordRepository PasswordRepository { get; }

        /// <summary>
        /// Gets the repository for managing roles.
        /// </summary>
        IRoleRepository RoleRepository { get; }

        /// <summary>
        /// Gets the repository for managing invitations.
        /// </summary>
        IInvitationRepository InvitationRepository { get; }

        /// <summary>
        /// Gets the repository for managing email-related operations.
        /// </summary>
        IEmailRepository EmailRepository { get; }

        /// <summary>
        /// Gets the repository for managing tokens.
        /// </summary>
        ITokenRepository TokenRepository { get; }
        
        /// <summary>
        /// Gets the repository for country-related operations.
        /// </summary>
        ICountryRepository CountryRepository { get; }
        int Complete();
    }
}
