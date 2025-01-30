using CRUDReactJSNetCore.Infrastructure.ContextDb;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace CRUDReactJSNetCore.Infrastructure
{
    public static class InfrastructureServicesRegister
    {
        public static IServiceCollection RegisterInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CRUDReactJSNetCoreDbContent>(opt => opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            return services;
        }
    }
}
