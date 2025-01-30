using MediatR;

namespace CRUDReactJSNetCore.Application.Feature.Funcionario.Query.ListFuncionario
{
    public class ListFuncionarioRequest : IRequest<IEnumerable<ListFuncionarioResponse>>
    {
        public int PageIndex { get; set; }
        public int PageCount { get; set; }
        public string Filtro { get; set; }
    }
}
