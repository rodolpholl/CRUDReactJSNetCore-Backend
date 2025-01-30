using CRUDReactJSNetCore.Domain.Entities;

namespace CRUDReactJSNetCore.Application.Repository
{
    public interface ICargoRepository
    {
        Task<List<Cargo>> ListCargos();
        Task<Cargo> GetCargoById(long cargoId);
    }
}
