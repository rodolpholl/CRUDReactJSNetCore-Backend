using MediatR;

namespace CRUDReactJSNetCore.Application.Feature.Funcionario.Query.ListGestores
{
    public class ListGestoresRequest : IRequest<IEnumerable<ListGestoresResponse>>
    {
        public int LevelCargo { get; set; }
    }
}
