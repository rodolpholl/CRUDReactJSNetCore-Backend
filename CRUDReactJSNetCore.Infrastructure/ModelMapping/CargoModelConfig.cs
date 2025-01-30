using CRUDReactJSNetCore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRUDReactJSNetCore.Infrastructure.ModelMapping
{
    public class CargoModelConfig : IEntityTypeConfiguration<Cargo>
    {
        public void Configure(EntityTypeBuilder<Cargo> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.Nome).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Level).IsRequired();
        }
    }
}
