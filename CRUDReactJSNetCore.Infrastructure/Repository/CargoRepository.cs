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

        public async Task<IEnumerable<Cargo>> ListCargos()
        => (await _dbContext.Cargos.Where(x => x.Level > 0 && x.Active == true).OrderBy(x => x.Level).ToListAsync()).AsEnumerable();
    }
}
