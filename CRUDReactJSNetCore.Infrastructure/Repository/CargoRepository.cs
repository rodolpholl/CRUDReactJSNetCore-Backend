using CRUDReactJSNetCore.Application.Repository;
using CRUDReactJSNetCore.Domain.Entities;
using CRUDReactJSNetCore.Infrastructure.ContextDb;
using Microsoft.EntityFrameworkCore;

namespace CRUDReactJSNetCore.Infrastructure.Repository
{
    public class CargoRepository : RepositoryBase<Cargo>, ICargoRepository
    {
        public CargoRepository(CRUDReactJSNetCoreDbContent dbContext) : base(dbContext)
        {
        }

        public Task<Cargo> GetCargoById(long cargoId)
        => GetById(cargoId);

        public Task<List<Cargo>> ListCargos()
        => _dbContext.Cargos.OrderBy(x => x.Level).ToListAsync();
    }
}
