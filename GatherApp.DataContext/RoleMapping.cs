using GatherApp.Contracts.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using GatherApp.Contracts.Enums;

namespace GatherApp.DataContext
{
    public class RoleMapping : IEntityTypeConfiguration<Role>
    {
        
        public void Configure(EntityTypeBuilder<Role> builder)
        {   
            builder.ToTable("roles");
            builder.HasKey(r => r.Id);
            builder.HasMany(r => r.Users)
                .WithOne(u => u.Role)
                .HasForeignKey(u => u.RoleId);
        }
    }
}
