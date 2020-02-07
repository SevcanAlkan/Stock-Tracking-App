using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NGA.Domain;

namespace NGA.Data.Mapping
{
    public class ParameterMap : IEntityTypeConfiguration<Parameter>
    {
        public void Configure(EntityTypeBuilder<Parameter> builder)
        {
            builder.ToTable("Parameter");

            builder.Property(c => c.Code).HasMaxLength(100);
            builder.Property(c => c.Name).HasMaxLength(100);
            builder.Property(c => c.GroupCode).HasMaxLength(10);
            builder.Property(c => c.OrderIndex).HasDefaultValue(0).IsRequired();

        }
    }
}
