using CRUDReactJSNetCore.Application.Repository;
using CRUDReactJSNetCore.Infrastructure.ContextDb;
using CRUDReactJSNetCore.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace CRUDReactJSNetCore.Infrastructure
{
    public static class InfrastructureServicesRegister
    {
        public static IServiceCollection RegisterInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            //Add DbContext
            services.AddDbContext<CRUDReactJSNetCoreDbContent>(opt => opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            //Add Repository
            services.AddScoped<IFuncionarioRepository, FuncionarioRepository>();
            services.AddScoped<ICargoRepository, CargoRepository>();

            return services;
        }
    }
}

