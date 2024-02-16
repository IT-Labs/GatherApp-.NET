using GatherApp.Contracts.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace GatherApp.DataContext
{
    public class InvitationMapping : IEntityTypeConfiguration<Invitation>
    {
        public void Configure(EntityTypeBuilder<Invitation> builder)
        {
            builder.ToTable("invitations");

            builder.HasKey(i => i.Id);

            builder.HasOne(i => i.User)
                .WithMany(u => u.Invitations)
                .HasForeignKey(i => i.UserId);

            builder.HasOne(i => i.Event)
                .WithMany(e => e.Invitations)
                .HasForeignKey(i => i.EventId);
        }
    }
}
