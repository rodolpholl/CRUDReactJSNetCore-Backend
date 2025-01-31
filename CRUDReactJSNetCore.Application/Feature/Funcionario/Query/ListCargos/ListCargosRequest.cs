using MediatR;

namespace CRUDReactJSNetCore.Application.Feature.Funcionario.Query.ListCargos
{
    public class ListCargosRequest : IRequest<IEnumerable<ListCargosResponse>>
    {
    }
}
