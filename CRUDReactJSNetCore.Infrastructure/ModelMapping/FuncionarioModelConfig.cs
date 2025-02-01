using CRUDReactJSNetCore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRUDReactJSNetCore.Infrastructure.ModelMapping
{
    public class FuncionarioModelConfig : IEntityTypeConfiguration<Funcionario>
    {
        public void Configure(EntityTypeBuilder<Funcionario> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.Nome).HasMaxLength(80).IsRequired();
            builder.HasIndex(x => x.Nome, "IDX_FUNCIONARIO_NOME");

            builder.Property(x => x.Email).HasMaxLength(150).IsRequired();
            builder.HasIndex(x => x.Email, "IDX_FUNCIONARIO_EMAIL");

            builder.Property(x => x.Active).HasDefaultValue(true).IsRequired();
            builder.HasIndex(x => x.Active, "IDX_FUNCIONARIO_ACTIVE");

            builder.Property(x => x.TelefoneString).HasColumnName("Telefone").HasMaxLength(1500).IsRequired();
            builder.Property(x => x.Senha).HasMaxLength(300);
            builder.Property(x => x.Documento).HasMaxLength(150).IsRequired();

            #region Relacionamentos

            builder.HasOne(x => x.Cargo)
                .WithMany()
                .HasForeignKey(x => x.CargoId);

            builder.HasOne(x => x.Gestor)
                .WithMany()
                .HasForeignKey(x => x.GestorId)
                .OnDelete(DeleteBehavior.Restrict);

            #endregion
        }
    }
}
