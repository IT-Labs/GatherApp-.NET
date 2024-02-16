using GatherApp.Contracts.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GatherApp.DataContext
{
    public class PasswordMapping : IEntityTypeConfiguration<PasswordToken>
        {
            public void Configure(EntityTypeBuilder<PasswordToken> builder)
            {
                builder.ToTable("passwordToken");
                builder.HasKey(u => u.Id);
            }
        }
    }
