using GatherApp.Contracts.Entities;
using Microsoft.EntityFrameworkCore;

namespace GatherApp.DataContext
{
    public class GatherAppContext : DbContext
    {
        public DbSet<Event> Events { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<PasswordToken> PasswordChange { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Invitation> Invitations { get; set; }
        public DbSet<Email> EmailConstants { get; set; }
        public DbSet<Token> Tokens { get; set; }
        public DbSet<Country> Countries { get; set; }

        public GatherAppContext(DbContextOptions<GatherAppContext> options) : base(options) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.ApplyConfiguration(new EventMapping());
            modelBuilder.ApplyConfiguration(new RoleMapping());
            modelBuilder.ApplyConfiguration(new UserMapping());
            modelBuilder.ApplyConfiguration(new PasswordMapping());
            modelBuilder.ApplyConfiguration(new InvitationMapping());
            modelBuilder.ApplyConfiguration(new EmailMapping());
            modelBuilder.ApplyConfiguration(new TokenMapping());
            modelBuilder.ApplyConfiguration(new CountryMapping());
            base.OnModelCreating(modelBuilder);
        }
        
    }
}
