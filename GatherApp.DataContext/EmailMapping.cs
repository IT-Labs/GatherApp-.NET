using GatherApp.Contracts.Entities;
using GatherApp.Contracts.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GatherApp.DataContext
{
    public class EmailMapping : IEntityTypeConfiguration<Email>
    {
        public void Configure(EntityTypeBuilder<Email> builder)
        {
            builder.ToTable("emails");
            builder.HasKey(x => x.Id);
        }
    }
}
