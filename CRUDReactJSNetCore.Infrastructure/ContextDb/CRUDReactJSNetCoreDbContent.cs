using CRUDReactJSNetCore.Domain.Entities;
using CRUDReactJSNetCore.Infrastructure.ModelMapping;
using Microsoft.EntityFrameworkCore;

namespace CRUDReactJSNetCore.Infrastructure.ContextDb
{
    public class CRUDReactJSNetCoreDbContent : DbContext
    {
        internal DbSet<Funcionario> Funcionarios { get; set; }
        internal DbSet<Cargo> Cargos { get; set; }

        public CRUDReactJSNetCoreDbContent(DbContextOptions<CRUDReactJSNetCoreDbContent> options) : base(options)
        => Task.Run(SeedDb).GetAwaiter().GetResult();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new CargoModelConfig());
            modelBuilder.ApplyConfiguration(new FuncionarioModelConfig());
        }

        private async Task SeedDb()
        {
            if (await Database.EnsureCreatedAsync())
            {
                if (!Cargos.AsEnumerable().Any())
                {
                    var arNewCargos = new List<Cargo>
                    {
                        new Cargo
                        {
                            Nome = "Administrador Sistema",
                            Level = 0
                        },
                        new Cargo
                        {
                             Nome = "Diretor",
                             Level = 1
                        },
                        new Cargo
                        {
                             Nome = "Gerente",
                             Level = 2
                        },
                        new Cargo
                        {
                             Nome = "Lider",
                             Level = 3
                        },
                        new Cargo
                        {
                             Nome = "Analista",
                             Level = 4
                        },
                        new Cargo
                        {
                             Nome = "Assistente",
                             Level = 5
                        },
                        new Cargo
                        {
                             Nome = "Treinee",
                             Level = 6
                        }

                    };

                    await Cargos.AddRangeAsync(arNewCargos);

                    if (!Funcionarios.AsEnumerable().Any())
                    {
                        var funcionario = new Funcionario
                        {
                            Nome = "Sys Admin",
                            Email = "admin@admin.com.br",
                            Active = true,
                            CargoId = 1,
                            Senha = "Admin123",
                            Documento = "123AdminDoc",
                            TelefoneString = "123456789",
                            DataCriacao = DateTime.Now
                        };
                        await Funcionarios.AddAsync(funcionario);

                    }


                    await SaveChangesAsync();
                }
            }
        }

    }
}
