using CRUDReactJSNetCore.Domain.Entities;

namespace CRUDReactJSNetCore.Application.Repository
{
    public interface ICargoRepository
    {
        Task<IEnumerable<Cargo>> ListCargos();
    }
}
