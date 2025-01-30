using MediatR;

namespace CRUDReactJSNetCore.Application.Feature.Funcionario.Query.GetFuncionarioById
{
    public class GetFuncionarioByIdRequest : IRequest<GetFuncionarioByIdResponse>
    {
        public string FuncionarioId { get; set; }
    }
}
