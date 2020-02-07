using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NGA.Domain;

namespace NGA.Data.Mapping
{
    public class GroupMap : IEntityTypeConfiguration<Group>
    {
        public void Configure(EntityTypeBuilder<Group> builder)
        {
            builder.ToTable("Group");

            builder.Property(c => c.Name).IsRequired().HasMaxLength(100);
            builder.Property(c => c.Description).HasMaxLength(250);
            builder.Property(c => c.IsMain).HasDefaultValue(false);
            builder.Property(c => c.IsPrivate).HasDefaultValue(false);
        }
    }
}
