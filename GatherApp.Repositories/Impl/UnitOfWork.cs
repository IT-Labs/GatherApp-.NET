using GatherApp.DataContext;

namespace GatherApp.Repositories.Impl
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly GatherAppContext _gatherAppContext;
        public IEventRepository EventRepository { get; private set; }
        public IUserRepository UserRepository { get; private set; }
        public IPasswordRepository PasswordRepository { get; private set; }
        public IRoleRepository RoleRepository { get; private set; }
        public IInvitationRepository InvitationRepository {get; private set; }
        public IEmailRepository EmailRepository { get; private set; }
        public ITokenRepository TokenRepository { get; private set; }
        public ICountryRepository CountryRepository { get; private set; }

        public UnitOfWork(GatherAppContext gatherAppContext, IEventRepository eventRepository, IUserRepository userRepository, 
            IPasswordRepository passwordRepository, IRoleRepository roleRepository, IInvitationRepository invitationRepository, 
            IEmailRepository emailRepository, ITokenRepository tokenRepository, ICountryRepository countryRepository)
        {
            _gatherAppContext = gatherAppContext;
            EventRepository = eventRepository;
            UserRepository = userRepository;
            PasswordRepository = passwordRepository;
            RoleRepository = roleRepository;
            InvitationRepository = invitationRepository;
            EmailRepository = emailRepository;
            TokenRepository = tokenRepository;
            CountryRepository = countryRepository;
        }
        public int Complete()
        {
            return _gatherAppContext.SaveChanges();
        }

        public void Dispose()
        {
            _gatherAppContext.Dispose();
        }
    }
}
